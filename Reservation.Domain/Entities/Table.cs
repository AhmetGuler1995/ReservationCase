using Reservation.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservation.Domain.Entities
{
    [Table("Table")]
    public class Table : BaseEntity
    {
       
        public int Number { get; set; }
        public int Capacity { get; set; }
    }
}
