using System.Net.Mail;
using System.Net;
using Reservation.Domain.Models.ConfigurationModels.Abstract;
using Reservation.Domain.Models.Infrastructure.Email;
using Reservation.Infrastructure.Email.Abstract;

namespace Reservation.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailConfigurationInformationModel _emailConfigurationInformationModel;
        public EmailSender(IEmailConfigurationInformationModel emailConfigurationInformationModel)
        {
            _emailConfigurationInformationModel = emailConfigurationInformationModel;
        }

        public bool Send(EmailSenderDto sendDto)
        {
           
            using SmtpClient smtp = new();
            try
            {
                MailAddress to = new(sendDto.EmailAddress);
                MailAddress from = new(_emailConfigurationInformationModel.MainEmailAddress);
                MailMessage email = new(from, to);
                email.Subject = sendDto.Subject;
                email.Body = sendDto.Body;
                smtp.Host = _emailConfigurationInformationModel.Host;
                smtp.Port = _emailConfigurationInformationModel.Port;
                smtp.Credentials = new NetworkCredential(_emailConfigurationInformationModel.MainEmailAddress, _emailConfigurationInformationModel.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = _emailConfigurationInformationModel.SSL;
                smtp.Send(email);
                return true;
            }
            catch 
            {
                return true;
            }
            finally
            {
                smtp.Dispose();
            }
        }
    }
}
