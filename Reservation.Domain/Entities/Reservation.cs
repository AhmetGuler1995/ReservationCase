using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reservation.Domain.Entities.Base;

namespace Reservation.Domain.Entities
{
    public class Reservation :BaseEntity
    {
        
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        [ForeignKey("Table")]
        public int TableNumber { get; set; }
        public virtual Table Table { get; set; }
    }
}
