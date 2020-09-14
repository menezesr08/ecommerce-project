using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
             /* AddScoped method refers to the lifetime of the service we are injecting. 
            In this case the service would be destroyed when the HTTP requests has finished.
            The scoped lifetime works in this format:
            - HTTP request comes in
            - Controller gets hit and creates an instance of repo
            - Repo gets data and sends it back to controller and then to client
            - HTTP request is now finished and this service is destroyed.
            */
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
             // Make sure you add below service after services.AddControllers() otherwise it wont work
            services.Configure<ApiBehaviorOptions>(options =>
            {   /*
                    This actionContext inside invalidmodel... contains the validation errors that is displayed
                    when the user has sent an invalid request. Here we can access these errors and reformat them so that
                    they are inside an array which is easier to read than json objects.  
                    
                */
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });;

            return services;
        }
    }
}