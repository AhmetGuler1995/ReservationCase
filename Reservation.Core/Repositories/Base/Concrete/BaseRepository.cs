using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Reservation.Core.Repositories.Base.Abstract;
using Reservation.Domain.Entities.Base;
using Reservation.Persistence;

namespace Reservation.Core.Repositories.Base.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public readonly ReservationDbContext ReservationDbContext;
        public BaseRepository(ReservationDbContext reservationDbContext)
        {
            ReservationDbContext = reservationDbContext;
        }
        public IList<TEntity> GetAll()
        {
            return ReservationDbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity GetById(int id)
        {
            return ReservationDbContext.Set<TEntity>().FirstOrDefault(x=>x.Id == id && x.Cancel == false);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return ReservationDbContext.Set<TEntity>().FirstOrDefault(expression);
        }

        public IList<TEntity> GetMany(Expression<Func<TEntity, bool>> expression)
        {
            return ReservationDbContext.Set<TEntity>().AsNoTracking().Where(expression).ToList();
        }

        public void Delete(TEntity entity)
        {
            ReservationDbContext.Set<TEntity>().Remove(entity);
        }

        public int Save(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.Cancel = false;
            ReservationDbContext.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public void Update(TEntity entity)
        {
            ReservationDbContext.Set<TEntity>().Update(entity);
        }

        public void SetCancel(TEntity entity)
        {
            entity.Cancel = true;
            ReservationDbContext.Set<TEntity>().Update(entity);
        }
    }
}
