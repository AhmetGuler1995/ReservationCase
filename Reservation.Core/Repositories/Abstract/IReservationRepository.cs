using Reservation.Core.Repositories.Base.Abstract;
using Reservation.Domain.Entities;
using System.Linq.Expressions;

namespace Reservation.Core.Repositories.Abstract
{
    public interface IReservationRepository : IBaseRepository<Reservation.Domain.Entities.Reservation>
    {
      
    }
}
