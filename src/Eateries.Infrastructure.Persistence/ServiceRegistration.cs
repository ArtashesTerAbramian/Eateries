using Eateries.Application.Interfaces;
using Eateries.Application.Interfaces.Repositories;
//using Eateries.Application.Interfaces.Repositories;
using Eateries.Infrastructure.Persistence.Contexts;
using Eateries.Infrastructure.Persistence.Repositories;
using Eateries.Infrastructure.Persistence.Repository;
using Eateries.Infrastructure.Persistence.Services;
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

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IAddressRepositoryAsync, AddressRepositoryAsync>();
            services.AddTransient<IMenuRepositoryAsync, MenuRepositoryAsync>();
            services.AddTransient<IEateryRepositoryAsync, EateryRepositoryAsync>();
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddTransient<ICuisineRepositoryAsync, CuisineRepositoryAsync>();
            services.AddTransient<IGenerateDishSuggestionAsync, GenerateDishSuggestionAsync>();
            services.AddTransient<IOrderRepositoryAsync, OrderRepositoryAsync>();
            services.AddTransient<IDishRepositoryAsync, DishRepositoryAsync>();
            /* services.AddTransient<IPositionRepositoryAsync, PositionRepositoryAsync>();
             services.AddTransient<IEmployeeRepositoryAsync, EmployeeRepositoryAsync>();*/


            #endregion Repositories
        }
    }
}