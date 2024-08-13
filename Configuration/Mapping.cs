using AahanaClinic.Database;
using AahanaClinic.Models;
using AahanaClinic.Utilities;
using AahanaClinic.ViewModels;
using Mapster;

namespace AahanaClinic.Configuration
{
    public class Mapping 
    {
        public static void MapEntityModel(IWebHostEnvironment environment, AppDbContext dbContext)
        {
            //TypeAdapterConfig<ReportViewModel, LabReport>.NewConfig().Map(dest => dest.FileId, src => src.File != null ? Utility.UploadFile(src.File, environment, dbContext) : null);
        }
    }
}
