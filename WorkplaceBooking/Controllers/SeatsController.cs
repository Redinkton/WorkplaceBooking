using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkplaceBooking.Services;

namespace WorkplaceBooking.Controllers
{
    [Authorize]
    public class SeatsController(ISeatService seatService) : Controller
    {
        private readonly ISeatService _seatService = seatService;

        [Route("FirstFloor")]
        public async Task<IActionResult> FirstFloor()
        {
            var occupiedSeats = await _seatService.GetOccupiedSeatsAsync();
            return View("FirstFloor", occupiedSeats);
        }

        [Route("SecondFloor")]
        public async Task<IActionResult> SecondFloor()
        {
            var occupiedSeats = await _seatService.GetOccupiedSeatsAsync();
            return View("SecondFloor", occupiedSeats);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveSeat(int seatNumber)
        {
            var userId = HttpContext.Items["UserId"] as string;
            if (!await _seatService.ReserveSeatAsync(seatNumber, userId))
            {
                TempData["Error"] = "The seat is already taken.";
            }
            return RedirectToAction(seatNumber >= 8 ? "FirstFloor" : "SecondFloor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FreeSeat(int seatNumber)
        {
            var userId = HttpContext.Items["UserId"] as string;

            if (!await _seatService.FreeSeatAsync(seatNumber, userId))
            {
                TempData["Error"] = "You cannot vacate this seat.";
            }
            return RedirectToAction(seatNumber < 8 ? "SecondFloor" : "FirstFloor");
        }
    }
}