using System.Security.Claims;

namespace WorkplaceBooking.Middleware
{
    public class SeatMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    context.Items["UserId"] = userId?.ToString();
                }
            }
            await _next(context);
        }
    }
}
