using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Repositories.Base.Concrete;
using Reservation.Persistence;

namespace Reservation.Core.Repositories.Concrete
{
    public class ReservationRepository :BaseRepository<Domain.Entities.Reservation>,IReservationRepository
    {
       
        public ReservationRepository(ReservationDbContext reservationDbContext) : base(reservationDbContext)
        {
        }
    }
}
