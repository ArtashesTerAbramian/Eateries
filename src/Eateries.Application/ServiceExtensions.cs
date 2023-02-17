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
    public static class ServiceExtensicons
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IModelHelper, ModelHelper>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IDataShapeHelper<Address>, DataShapeHelper<Address>>();
            services.AddScoped<IDataShapeHelper<Dish>, DataShapeHelper<Dish>>();
            services.AddScoped<IDataShapeHelper<Eatery>, DataShapeHelper<Eatery>>();
            services.AddScoped<IDataShapeHelper<Cuisine>, DataShapeHelper<Cuisine>>();
            services.AddScoped<IDataShapeHelper<Ingredient>, DataShapeHelper<Ingredient>>();
            services.AddScoped<IDataShapeHelper<DishIngredients>, DataShapeHelper<DishIngredients>>();
            //services.AddScoped<IMockData, MockData>();
        }
    }
}