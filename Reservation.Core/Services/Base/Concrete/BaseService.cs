using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Services.Base.Abstract;

namespace Reservation.Core.Services.Base.Concrete
{
    public class BaseService : IBaseService
    {
        private readonly ICommonRepository _commonRepository;

        public BaseService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public void SaveChanges()
        {
            _commonRepository.SaveChange();
        }
    }
}
