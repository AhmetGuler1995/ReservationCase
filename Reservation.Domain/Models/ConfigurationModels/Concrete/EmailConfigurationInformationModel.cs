using Reservation.Domain.Models.ConfigurationModels.Abstract;

namespace Reservation.Domain.Models.ConfigurationModels.Concrete
{
    public class EmailConfigurationInformationModel : IEmailConfigurationInformationModel
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string MainEmailAddress { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
