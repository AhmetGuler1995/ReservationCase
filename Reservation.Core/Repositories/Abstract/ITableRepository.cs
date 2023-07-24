using Reservation.Domain.Entities;
using Reservation.Domain.Models.RequestDtos.Table;
using System.Linq.Expressions;
using Reservation.Core.Repositories.Base.Abstract;

namespace Reservation.Core.Repositories.Abstract
{
    public interface ITableRepository :IBaseRepository<Table>
    {
        List<Table> GetTableListWithoutReservation(
            GetTableListWithoutReservationRequestDto requestGetTableListWithoutReservation);
    }
}
