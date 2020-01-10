using System.Threading.Tasks;
using PlusUltra.SendPulse.ApiClient.ViewModels;
using Refit;

namespace PlusUltra.SendPulse.ApiClient
{
    public interface ISendPulseSMTPClient
    {
        [Post("/smtp/emails")]
        Task SendEmailAsync([Body]SendEmailRequest request);
    }
}