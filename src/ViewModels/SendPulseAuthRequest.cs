namespace PlusUltra.SendPulse.ApiClient.ViewModels
{
    public class SendPulseAuthRequest
    {
        public string grant_type { get; set; } = "client_credentials";
        public string client_id { get; set; }   
        public string client_secret { get; set; }
    }
}