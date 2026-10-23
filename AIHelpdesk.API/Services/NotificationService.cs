using AIHelpdesk.API.Interfaces;

namespace AIHelpdesk.API.Services
{
    public class EmailNotificationService : INotification
    {
        private readonly ILogger<EmailNotificationService> _logger;
        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;

        }
        public string Send()
        {
            _logger.LogInformation("Email Send");
            return "Email Send";
        }
    }
    public class SMSNotificationService : INotification
    {
        private readonly ILogger<SMSNotificationService> _logger;
        public SMSNotificationService(ILogger<SMSNotificationService> logger)
        {
            _logger = logger;

        }
        public string Send()
        {
            _logger.LogInformation("Sms Send");
            return "SMS Send";
        }
    }
}
