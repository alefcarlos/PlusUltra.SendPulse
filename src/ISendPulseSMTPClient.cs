using System.Threading.Tasks;
using PlusUltra.SendPulse.ViewModels;
using Refit;

namespace PlusUltra.SendPulse
{
    public interface ISendPulseSMTPClient
    {
        [Post("/smtp/emails")]
        Task SendEmailAsync([Body]SendEmailRequest request);
    }
}