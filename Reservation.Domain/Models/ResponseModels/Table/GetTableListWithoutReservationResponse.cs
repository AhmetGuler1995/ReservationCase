namespace Reservation.Domain.Models.ResponseModels.Table
{
    public class GetTableListWithoutReservationResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Cancel { get; set; }
    }
}
