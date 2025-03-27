using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WorkplaceBooking.Middleware;
using Xunit;

namespace WorkplaceBooking.Tests
{
    public class SeatMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_UserAuthenticated_SetsUserIdAndCallsNext()
        {
            var context = new DefaultHttpContext();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user")
            };
            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));

            bool nextCaller = false;
            RequestDelegate next = ctx =>
            {
                nextCaller = true;
                return Task.CompletedTask;
            };

            var middleware = new SeatMiddleware(next);

            await middleware.InvokeAsync(context);
            Assert.True(nextCaller);
            Assert.True(context.Items.ContainsKey("UserId"));
            Assert.Equal("test-user", context.Items["UserId"]);
        }
    }
}
