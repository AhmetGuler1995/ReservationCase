namespace Reservation.Domain.Models.GeneralModels
{
    public class GeneralListResponse<T> where T : class
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public List<T> Items { get; set; }
    }
}
