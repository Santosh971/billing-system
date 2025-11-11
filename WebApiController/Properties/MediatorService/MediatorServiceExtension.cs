using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using Application.Queries.ProductQueries;
using Application.Queries.UserQueries;
using MediatR;
using System.Reflection;

namespace WebApiController.Properties.MediatorService
{
    public static class MediatorServiceExtension
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            // User Confguration 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddUserCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateUserCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteUserByIdCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetUserByUserIdQuery).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllUsersQuery).GetTypeInfo().Assembly));


            // Product Confguration 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddProductCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateProductCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DeleteProductByProductIdCommand).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetProductByProductIdQuery).GetTypeInfo().Assembly));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllProductsQuery).GetTypeInfo().Assembly));





            return services;

        }
    }
}
