using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PlusUltra.SendPulse.ViewModels;

namespace PlusUltra.SendPulse.MessageHandlers
{
    internal class SendPulseAuthenticationHeaderHandler : DelegatingHandler
    {
        public SendPulseAuthenticationHeaderHandler(ISendPulseAuthClient authClient, IOptions<SendPulseSettings> settings)
        {
            this.authClient = authClient;
            this.settings = settings.Value;
        }

        private readonly ISendPulseAuthClient authClient;
        private readonly SendPulseSettings settings;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var form = new SendPulseAuthRequest
            {
                client_id = settings.ClientId,
                client_secret = settings.ClientSecret
            };

            //Obter o token
            var token = await authClient.LoginAsync(form);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}