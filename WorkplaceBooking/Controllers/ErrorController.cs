using Microsoft.AspNetCore.Mvc;

namespace WorkplaceBooking.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
