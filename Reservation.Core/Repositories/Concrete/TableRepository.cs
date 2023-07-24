using Microsoft.EntityFrameworkCore;
using Reservation.Core.Repositories.Abstract;
using Reservation.Domain.Entities;
using Reservation.Domain.Models.RequestDtos.Table;
using Reservation.Persistence;
using System.Linq.Expressions;
using Reservation.Core.Repositories.Base.Concrete;

namespace Reservation.Core.Repositories.Concrete
{
    public class TableRepository : BaseRepository<Table>,ITableRepository
    {

        public TableRepository(ReservationDbContext reservationDbContext) : base(reservationDbContext)
        {
        }
        public List<Table> GetTableListWithoutReservation(GetTableListWithoutReservationRequestDto requestGetTableListWithoutReservation)
        {
            return ReservationDbContext.Tables.Where(x => x.Capacity >= requestGetTableListWithoutReservation.NumberOfGuest)
                .GroupJoin(ReservationDbContext.Reservations.Where(x => x.ReservationDate.Date == requestGetTableListWithoutReservation.ReservationDate.Value.Date), table => table.Id, reservation => reservation.TableNumber, (table, reservation) => new { table, reservation })
                .Select(x => x.table)
                .ToList();
        }

       
    }
}
