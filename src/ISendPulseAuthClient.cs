using System.Threading.Tasks;
using PlusUltra.SendPulse.ViewModels;
using Refit;

namespace PlusUltra.SendPulse
{
    internal interface ISendPulseAuthClient
    {
        [Post("/oauth/access_token")]
        Task<SendPulseTokenResponse> LoginAsync([Body(BodySerializationMethod.UrlEncoded)]SendPulseAuthRequest request);
    }
}