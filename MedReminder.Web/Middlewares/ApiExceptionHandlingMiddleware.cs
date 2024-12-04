namespace MedReminder.Web.Middlewares;

public class ApiExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;

	public ApiExceptionHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsJsonAsync(new { message = ex.Message });
		}

	}

}