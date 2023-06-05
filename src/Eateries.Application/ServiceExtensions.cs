using Eateries.Application.Behaviours;
using Eateries.Application.Helpers;
using Eateries.Application.Interfaces;
using Eateries.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Eateries.Application.Interfaces.Repositories;

namespace Eateries.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IModelHelper, ModelHelper>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IDataShapeHelper<Address>, DataShapeHelper<Address>>();
            services.AddScoped<IDataShapeHelper<Menu>, DataShapeHelper<Menu>>();
            services.AddScoped<IDataShapeHelper<Eatery>, DataShapeHelper<Eatery>>();
            services.AddScoped<IDataShapeHelper<User>, DataShapeHelper<User>>();
            services.AddScoped<IDataShapeHelper<Cuisine>, DataShapeHelper<Cuisine>>();
            services.AddScoped<IDataShapeHelper<Order>, DataShapeHelper<Order>>();
            services.AddScoped<IDataShapeHelper<Dish>, DataShapeHelper<Dish>>();
            services.AddScoped<IDataShapeHelper<Ingredient>, DataShapeHelper<Ingredient>>();
            services.AddScoped<IDataShapeHelper<DishGrade>, DataShapeHelper<DishGrade>>();
            services.AddScoped<IDataShapeHelper<MenuDish>, DataShapeHelper<MenuDish>>();
            //services.AddScoped<IMockData, MockData>();
        }
    }
}