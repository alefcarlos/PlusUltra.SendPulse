using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using PlusUltra.DistributedCache;

namespace PlusUltra.SendPulse.ApiClient.MessageHandlers
{
    internal class SendPulseAuthenticationHeaderHandler : DelegatingHandler
    {
        public SendPulseAuthenticationHeaderHandler(ISendPulseAuthClient authClient, IOptions<SendPulseSettings> settings, IDistributedCache cache)
        {
            this.authClient = authClient;
            this.settings = settings.Value;
            this.cache = cache;
        }

        private readonly ISendPulseAuthClient authClient;
        private readonly SendPulseSettings settings;
        private readonly IDistributedCache cache;

        const string CACHE_KEY = "sendpulse_key";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var form = new SendPulseAuthRequest
            {
                client_id = settings.ClientId,
                client_secret = settings.ClientSecret
            };

            //Tentar obter token do cache, senão realizar request
            var token = await cache.GetObjectAsync<SendPulseTokenResponse>(CACHE_KEY);

            if (token is null)
            {
                //fazer request  do token e salvar no cache
                token = await authClient.LoginAsync(form);
                await cache.SetObjectAsync(CACHE_KEY, token, TimeSpan.FromSeconds(token.expires_in - 60 * 5));
            }


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}