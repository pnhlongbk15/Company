namespace Business.Configuration
{
    public class MailConfiguration
    {
        public string Display { get; set; }
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
