namespace Hairdresser.Api.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
