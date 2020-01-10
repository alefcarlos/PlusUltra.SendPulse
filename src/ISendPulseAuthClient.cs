using System.Threading.Tasks;
using PlusUltra.SendPulse.ApiClient.ViewModels;
using Refit;

namespace PlusUltra.SendPulse.ApiClient
{
    internal interface ISendPulseAuthClient
    {
        [Post("/oauth/access_token")]
        Task<SendPulseTokenResponse> LoginAsync([Body(BodySerializationMethod.UrlEncoded)]SendPulseAuthRequest request);
    }
}