using AahanaClinic.Models;
using AahanaClinic.Database;

namespace AahanaClinic.Utilities
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

        public static async Task<int> UploadFile(IFormFile file, AppDbContext dbContext, IWebHostEnvironment environment, int fileId = 0)
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
            var payload = await dbContext.FileStorage.FindAsync(fileId);
            if (payload == null)
            {
                payload = new FileStorage
                {
                    Name = fileName,
                    Url = $"FileStorage/" // Assuming you want to store the relative path in the database
                };

                dbContext.FileStorage.Add(payload);
            }
            else
            {
                // Determine the file path
                var deletePath = Path.Combine(environment.WebRootPath, "FileStorage", payload.Name);

                // Check if the file exists, then delete it
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                }
                payload.Name = fileName;

                dbContext.FileStorage.Update(payload);
            }
            await dbContext.SaveChangesAsync();

            return payload.Id;
        }

        public static async Task DeleteFile(int fileId, AppDbContext dbContext, IWebHostEnvironment environment)
        {
            // Retrieve the file record from the database based on fileId
            var payload = await dbContext.FileStorage.FindAsync(fileId);
            if (payload == null)
            {
                throw new ArgumentException("File not found");
            }

            // Determine the file path
            var filePath = Path.Combine(environment.WebRootPath, "FileStorage", payload.Name);

            // Check if the file exists, then delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Remove the file record from the database
            dbContext.FileStorage.Remove(payload);
            await dbContext.SaveChangesAsync();
        }

    }
}
