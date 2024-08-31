using AahanaClinic.Models;
using AahanaClinic.ViewModels;
using Mapster;

namespace AahanaClinic
{
    public static class Mappings
    {
        public static void Register()
        {
            TypeAdapterConfig<SettingsViewModel, Settings>.NewConfig()
                .Ignore(i => i.Logo).Ignore(i => i.Signature);

            TypeAdapterConfig<Settings, SettingsViewModel>.NewConfig()
                .Ignore(i => i.Logo).Ignore(i => i.Signature)
                .Map(d => d.LogoUrl, s => $"{s.LogoFile.Url}{s.LogoFile.Name}")
                .Map(d => d.SignatureUrl, s => $"{s.SignatureFile.Url}{s.SignatureFile.Name}");

            TypeAdapterConfig<PackageViewModel, Package>.NewConfig().Map(d => d.ModeId, s => s.Mode)
                .Ignore(i => i.VisitBalance).Ignore(i => i.AveragePrice);

            TypeAdapterConfig<Package, PackageViewModel>.NewConfig().Map(d => d.Mode, s => s.ModeId);
        }
    }
}
