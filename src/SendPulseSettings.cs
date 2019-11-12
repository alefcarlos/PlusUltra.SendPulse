namespace PlusUltra.SendPulse.ApiClient
{
    public class SendPulseSettings
    {
        public string Uri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public SenderAddress Sender { get; set; }
    }
    public class SenderAddress
    {
        public string Name { get; set; }    
        public string Email { get; set; }
    }
}