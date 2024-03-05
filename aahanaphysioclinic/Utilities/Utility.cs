using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using aahanaphysioclinic.Model;
using aahanaphysioclinic.Data;

namespace aahanaphysioclinic.Utilities
{
    public static class Utility
    {
        public static DateTime GetDateFromYearMonthDay(int Year, int Month, int Day)
        {
            return new DateTime(Year, Month, Day, 0, 0, 0, 0);
        }

        public static DateTime GetTimeFromHoursMinutes(int Hours, int Minutes, string Meridiem)
        {
            if (Meridiem == "PM" && Hours < 12)
            {
                Hours += 12;
            }
            else if (Meridiem == "AM" && Hours == 12)
            {
                Hours = 0;
            }

            return new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, Hours, Minutes, 0);
        }

        public static async Task<int> UploadFile(IFormFile file, IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }

            // Get the file extension from the provided file variable
            var fileExtension = Path.GetExtension(file.FileName);

            // Generate a unique file name with extension
            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            // Determine the file path
            var filePath = Path.Combine(environment.WebRootPath, "FileStorage", fileName);

            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Record in the database
            var fileRecord = new FileStorage
            {
                FileName = fileName,
                FileURL = $"FileStorage/" // Assuming you want to store the relative path in the database
            };

            dbContext.FileStorage.Add(fileRecord);
            await dbContext.SaveChangesAsync();

            return fileRecord.FileId;
        }
        public static async Task DeleteFile(int fileId, ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            // Retrieve the file record from the database based on fileId
            var fileRecord = await dbContext.FileStorage.FindAsync(fileId);
            if (fileRecord == null)
            {
                throw new ArgumentException("File not found");
            }

            // Determine the file path
            var filePath = Path.Combine(environment.WebRootPath, "FileStorage", fileRecord.FileName);

            // Check if the file exists, then delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove the file record from the database
            dbContext.FileStorage.Remove(fileRecord);
            await dbContext.SaveChangesAsync();
        }

    }
}
