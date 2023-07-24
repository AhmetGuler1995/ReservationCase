namespace Reservation.Domain.Models.GeneralModels
{
    public class GeneralDataResponse<T> where T : class
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        
    }
}
