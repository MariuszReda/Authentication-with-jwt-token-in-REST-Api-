using Hairdresser.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Hairdresser.Api.Installers
{
    public class DbInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
