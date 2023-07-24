using Microsoft.AspNetCore.Mvc;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Base.Abstract;
using Reservation.Domain.Models.RequestDtos.Reservation;

namespace Reservation.Api.Controllers
{
    [Route("Reservation")]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IBaseService baseService, IReservationService reservationService) : base(baseService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        [Route("SaveReservation")]
        public IActionResult SaveReservation([FromBody] SaveReservationRequestDto request)
        {
            var serviceResponse = _reservationService.SaveReservation(request);
            return Ok(serviceResponse);
        }
        [HttpPost]
        [Route("UpdateReservation")]
        public IActionResult UpdateReservation([FromBody] UpdateReservationRequestDto request)
        {
            var serviceResponse = _reservationService.UpdateReservation(request);
            return Ok(serviceResponse);
        }
        
    }
}
