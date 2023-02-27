using Hairdresser.Api.Services;

namespace Hairdresser.Api.Installers
{
    public static class InstallExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installer = typeof(Program).Assembly.ExportedTypes.Where(x =>
            typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installer.ForEach(installer => installer.Install(services, configuration));

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IPostService, PostService>();

        }
        

    }
}
