using MedReminder.Web.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MedReminder.Web.Filters;

public class RedirectUnauthorizedAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var cookieService = context.HttpContext.RequestServices.GetService<ICookieService>();
		if (cookieService?.GetJwtToken() == null)
		{
			context.Result = new RedirectToActionResult("Login", "Account", null);
		}
	}
}
