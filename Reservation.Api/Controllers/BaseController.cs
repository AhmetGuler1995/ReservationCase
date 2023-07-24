using Microsoft.AspNetCore.Mvc;
using Reservation.Core.Services.Base.Abstract;

namespace Reservation.Api.Controllers
{
    public class BaseController  :ControllerBase
    {
        private readonly IBaseService _baseService;
        public IBaseService BaseService => _baseService;
        public BaseController(IBaseService baseService)
        {
            _baseService = baseService;
        }
    }
}
