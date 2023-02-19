using Eateries.Application.Behaviours;
using Eateries.Application.Helpers;
using Eateries.Application.Interfaces;
using Eateries.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            //services.AddScoped<IMockData, MockData>();
        }
    }
}