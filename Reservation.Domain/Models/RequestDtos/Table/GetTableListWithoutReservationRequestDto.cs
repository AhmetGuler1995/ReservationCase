namespace Reservation.Domain.Models.RequestDtos.Table
{
    public class GetTableListWithoutReservationRequestDto
    {
        public DateTime? ReservationDate { get; set; }
        public int NumberOfGuest { get; set; }
    }
}
