using Bitzen.Veiculos.Api.Extensions;
using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Notifications;
using Bitzen.Veiculos.Business.Services;
using Bitzen.Veiculos.Data.Context;
using Bitzen.Veiculos.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bitzen.Veiculos.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IOportunidadeRepository, OportunidadeRepository>();
            services.AddScoped<IOportunidadeLogRepository, OportunidadeLogRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IVendedorCargoRepository, VendedorCargoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IOportunidadeService, OportunidadeService>();
            services.AddScoped<IOportunidadeLogService, OportunidadeLogService>();
            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<IVendedorCargoService, VendedorCargoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}