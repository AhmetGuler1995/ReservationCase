using Reservation.Core.Services.Base.Abstract;
using Reservation.Domain.Models.GeneralModels;
using Reservation.Domain.Models.RequestDtos.Reservation;
using Reservation.Domain.Models.ResponseModels.Reservation;

namespace Reservation.Core.Services.Abstract
{
    public interface IReservationService : IBaseService
    {
        public GeneralDataResponse<SaveReservationResponse> SaveReservation(
            SaveReservationRequestDto requestSaveReservation);
        public GeneralResponse UpdateReservation(UpdateReservationRequestDto  requestUpdateReservation);
        public GeneralResponse DeleteReservation(DeleteReservationRequestDto requestDeleteReservation);
        public GeneralResponse SetActiveReservation(SetActiveReservationRequestDto requestSetActiveReservation);
        public GeneralResponse SetCancelReservation(SetCancelReservationRequestDto requestSetCancelReservation);
    }
}
