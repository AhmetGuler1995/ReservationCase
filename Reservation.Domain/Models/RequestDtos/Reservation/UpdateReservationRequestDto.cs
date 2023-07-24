namespace Reservation.Domain.Models.RequestDtos.Reservation
{
    public class UpdateReservationRequestDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int TableNumber { get; set; }
    }
}
