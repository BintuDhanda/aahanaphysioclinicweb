using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AahanaClinic.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public BlogsController(AppDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }
        // GET: BlogsController
        public async Task<ActionResult> Index()
        {
            var query = _context.Blogs.AsQueryable();
            var blogs = await query.ToListAsync();
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError("", TempData["Error"]?.ToString() ?? "");
            }
            return View(blogs);
        }

        // GET: BlogsController/Details/5
        public ActionResult Details(int id)
        {
            var detail = _context.Blogs.FirstOrDefault(g => g.Id == id);
            return View(detail);
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: BlogsController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] BlogViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Invalid request");
                }
                var blog = payload.Adapt<Blog>();
                var user = await _userManager.GetUserAsync(User);
                blog.CreatedBy = user.Id;
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Create));
            }
            return Redirect($"/Blogs/Index");
        }

        // GET: BlogsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(g => g.Id == id);
            var finalResponse = blog.Adapt<BlogViewModel>();
            return View(finalResponse);
        }

        // POST: BlogsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit([FromForm] BlogViewModel payload)
        {
            try
            {
                if (!ModelState.IsValid && payload.Id != null)
                {
                    throw new Exception("Invalid request");
                }
                var blog = await _context.Blogs.FindAsync(payload.Id);
                if (blog == null)
                {
                    throw new Exception("Not found");
                }
                var user = await _userManager.GetUserAsync(User);
                blog.Title = payload.Title;
                blog.Description = payload.Description;
                blog.MetaTitle = payload.MetaTitle;
                blog.MetaKeyword = payload.MetaKeyword;
                blog.MetaDescription = payload.MetaDescription;
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var blog = await _context.Blogs.FirstOrDefaultAsync(g => g.Id == payload.Id);
                var finalResponse = blog.Adapt<BlogViewModel>();
                return View(finalResponse);
            }
            return Redirect($"/Blogs/Index");
        }

        // GET: BlogsController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            try
            {
                if (blog == null)
                {
                    ModelState.AddModelError("", "Not found");
                }
                else
                {
                    _context.Blogs.Remove(blog);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Redirect($"/Blogs/Index");
        }
    }
}
