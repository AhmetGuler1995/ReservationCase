using System.Linq.Expressions;
using Reservation.Domain.Entities.Base;

namespace Reservation.Core.Repositories.Base.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IList<TEntity> GetMany(Expression<Func<TEntity, bool>> expression);
        void Delete(TEntity entity);
        int Save(TEntity entity);
        void Update(TEntity entity);
        void SetCancel( TEntity entity);
    }
}
