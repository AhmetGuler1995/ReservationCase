namespace Reservation.Domain.Models.ConfigurationModels.Abstract
{
    public interface IEmailConfigurationInformationModel
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string MainEmailAddress { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
