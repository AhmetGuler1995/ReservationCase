using Microsoft.AspNetCore.Mvc.Filters;
using Reservation.Api.Controllers;

namespace Reservation.Api.Filters
{
    public class DbContextTransactionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is BaseController baseController)
            {
                if (context.Exception == null)
                    baseController.BaseService.SaveChanges();
            }
            base.OnActionExecuted(context);
        }
    }
}
