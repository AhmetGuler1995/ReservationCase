using Reservation.Core.Repositories.Abstract;
using Reservation.Persistence;

namespace Reservation.Core.Repositories.Concrete
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ReservationDbContext _reservationDbContext;

        public CommonRepository(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }
        public void SaveChange()
        {
            _reservationDbContext.SaveChanges();
        }
    }
}
