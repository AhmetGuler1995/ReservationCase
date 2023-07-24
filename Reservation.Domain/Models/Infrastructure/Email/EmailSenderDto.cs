namespace Reservation.Domain.Models.Infrastructure.Email
{
    public class EmailSenderDto
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string EmailAddress { get; set; }
    }
}
