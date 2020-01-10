using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlusUltra.SendPulse.ViewModels;
using Refit;

namespace PlusUltra.SendPulse.Services
{
    public class SendPulseEmailServices
    {
        public SendPulseEmailServices(ILogger<SendPulseEmailServices> logger, ISendPulseSMTPClient sendPulseSMTPClient, IOptions<SendPulseSettings> settings)
        {
            this.sendPulseSMTPClient = sendPulseSMTPClient;
            this.settings = settings.Value;
            this.logger = logger;
        }

        private readonly ISendPulseSMTPClient sendPulseSMTPClient;
        private readonly SendPulseSettings settings;
        private readonly ILogger logger;

        public async Task SendTemplate(int templateId, string name, Dictionary<string, string> variables, string to, string subject)
        {
            var address = new Address(settings.Sender.Name, settings.Sender.Email);
            var toAddress = new Address(name, to);

            var email = SendEmailRequest.CreateTemplateEmail(address, toAddress, subject, templateId, variables);

            try
            {
                await sendPulseSMTPClient.SendEmailAsync(email);
            }
            catch (ApiException ex)
            {
                using (logger.BeginScope($"Envio de email {to}"))
                {
                    logger.LogCritical(ex, "Ocorreu erro ao tentar enviar e-mail.");
                    logger.LogError($"Resposta do SendPulse: {ex.Content}");
                };

                throw;
            }
        }

        public async Task SendText(string name, string to, string subject, string content)
        {
            var address = new Address(settings.Sender.Name, settings.Sender.Email);
            var toAddress = new Address(name, to);

            var email = SendEmailRequest.CreateTextEmail(address, toAddress, subject, content);

            try
            {
                await sendPulseSMTPClient.SendEmailAsync(email);
            }
            catch (ApiException ex)
            {
                using (logger.BeginScope($"Envio de email {to}"))
                {
                    logger.LogCritical(ex, "Ocorreu erro ao tentar enviar e-mail.");
                    logger.LogError($"Resposta do SendPulse: {ex.Content}");
                };

                throw;
            }
        }
    }
}