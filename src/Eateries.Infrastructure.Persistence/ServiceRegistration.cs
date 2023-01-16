using Eateries.Application.Interfaces;
//using Eateries.Application.Interfaces.Repositories;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repositories;
using Eateries.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eateries.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")));
            }

            #region Repositories

            // services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            /* services.AddTransient<IPositionRepositoryAsync, PositionRepositoryAsync>();
             services.AddTransient<IEmployeeRepositoryAsync, EmployeeRepositoryAsync>();*/


            #endregion Repositories
        }
    }
}