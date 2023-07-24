using Reservation.Domain.Models.Infrastructure.Email;

namespace Reservation.Infrastructure.Email.Abstract
{
    public interface IEmailSender
    {
        bool Send(EmailSenderDto sendDto);
    }
}
