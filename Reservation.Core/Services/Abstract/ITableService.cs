using Reservation.Core.Services.Base.Abstract;
using Reservation.Domain.Models.GeneralModels;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Domain.Models.ResponseModels.Table;

namespace Reservation.Core.Services.Abstract
{
    public interface ITableService : IBaseService
    {
        GeneralDataResponse<SaveTableResponse> SaveTable(SaveTableRequestDto requestSaveTable);
        GeneralResponse UpdateTable(UpdateTableRequestDto requestUpdateTable);
        GeneralResponse SetActiveTable(SetActiveTableRequestDto requestSetActiveTable);
        GeneralResponse SetCancelTable(SetCancelTableRequestDto requestSetCancelTable);
        GeneralResponse DeleteTable(DeleteTableRequestDto requestDeleteTable);
        GeneralListResponse<GetTableListWithoutReservationResponse> GetTableListWithoutReservation(
            GetTableListWithoutReservationRequestDto requestGetTableListWithoutReservation);
        GeneralDataResponse<GetTableResponse> GetTable(GetTableRequestDto requestGetTable);

    }
}
