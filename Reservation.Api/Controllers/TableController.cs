using Microsoft.AspNetCore.Mvc;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Base.Abstract;
using Reservation.Domain.Models.RequestDtos.Table;

namespace Reservation.Api.Controllers
{
    [Route("Table")]
    public class TableController : BaseController
    {
        private readonly ITableService _tableService;
        public TableController(IBaseService baseService, ITableService tableService) : base(baseService)
        {
            _tableService = tableService;
        }
        [HttpPost]
        [Route("SaveTable")]
        public IActionResult SaveTable([FromBody] SaveTableRequestDto request)
        {
            var serviceResponse = _tableService.SaveTable(request);
            return Ok(serviceResponse);
        }
        [HttpPost]
        [Route("UpdateTable")]
        public IActionResult UpdateTable([FromBody] UpdateTableRequestDto request)
        {
            var serviceResponse = _tableService.UpdateTable(request);
            return Ok(serviceResponse);
        }
        [HttpPost]
        [Route("SetActiveTable")]
        public IActionResult SetActiveTable([FromBody] SetActiveTableRequestDto request)
        {
            var serviceResponse = _tableService.SetActiveTable(request);
            return Ok(serviceResponse);
        }
        [HttpPost]
        [Route("SetCancelTable")]
        public IActionResult SetCancelTable([FromBody] SetCancelTableRequestDto request)
        {
            var serviceResponse = _tableService.SetCancelTable(request);
            return Ok(serviceResponse);
        }
        [HttpPost]
        [Route("DeleteTable")]
        public IActionResult DeleteTable([FromBody] DeleteTableRequestDto request)
        {
            var serviceResponse = _tableService.DeleteTable(request);
            return Ok(serviceResponse);
        }
        [HttpGet]
        [Route("GetTableListWithoutReservation")]
        public IActionResult GetTableListWithoutReservation([FromBody]GetTableListWithoutReservationRequestDto request)
        {
            var serviceResponse = _tableService.GetTableListWithoutReservation(request);
            return Ok(serviceResponse);
        }
        [HttpGet]
        [Route("GetTable")]
        public IActionResult GetTable([FromBody] GetTableRequestDto request)
        {
            var serviceResponse = _tableService.GetTable(request);
            return Ok(serviceResponse);
        }
    }
}
