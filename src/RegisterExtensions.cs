using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PlusUltra.ApiClient;
using PlusUltra.SendPulse.ApiClient.MessageHandlers;
using PlusUltra.SendPulse.ApiClient.Services;

namespace PlusUltra.SendPulse.ApiClient
{
    public static class RegisterExtensions
    {
        public static IServiceCollection AddSendPulse(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SendPulseSettings>(configuration.GetSection(nameof(SendPulseSettings)));
            var configs = services.BuildServiceProvider().GetRequiredService<IOptions<SendPulseSettings>>().Value;

            services.AddTransient<SendPulseAuthenticationHeaderHandler>();

            services.AddApiClient<ISendPulseSMTPClient>(c => c.BaseAddress = new Uri(configs.Uri))
                .AddHttpMessageHandler<SendPulseAuthenticationHeaderHandler>();

            services.AddApiClient<ISendPulseAuthClient>(c => c.BaseAddress = new Uri(configs.Uri));
            
            services.AddScoped<SendPulseEmailServices>();

            return services;
        }
    }
}