using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkplaceBooking.Migrations;
using WorkplaceBooking.Services;

namespace WorkplaceBooking.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("admin")]
    public class AdminController(IAdminService adminService, ISeatService seatService, IUserService userService) : Controller
    {
        private readonly IAdminService _adminService = adminService;
        private readonly ISeatService _seatService = seatService;
        private readonly IUserService _userService = userService;


        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var users = await _userService.GetUsersAsync();

            var allSeats = await _seatService.GetAllSeatsAsync();
            var freeSeats = allSeats.Where(s => s.Value == null).ToList();
            var occupiedSeats = allSeats.Where(s => s.Value != null).ToList();
            var bookings = await _seatService.GetAllBookingsAsync();

            var allSeatNumbers = allSeats.Keys.ToList();

            var model = new
            {
                Users = users,
                FreeSeats = freeSeats,
                OccupiedSeats = occupiedSeats,
                Bookings = bookings.Select(b => new
                {
                    b.Name,
                    b.Email,
                    SeatNumber = b.Seat?.Number ?? 0 
                }).ToList(),
                AllSeatNumbers = allSeatNumbers
            };
            return View(model);
        }

        [HttpPost("reserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveSeatForUser(string userEmail, int seatNumber)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _adminService.AdminReserveSeat(userEmail, seatNumber);
            stopwatch.Stop();
            Console.WriteLine($"\n Время выполнения: {stopwatch.ElapsedMilliseconds} мс\n");


            return RedirectToAction("Dashboard");
        }

        [HttpPost("cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelSeatForUser(string userEmail)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _adminService.AdminCancelBooking(userEmail);
            stopwatch.Stop();
            Console.WriteLine($"\n Время выполнения: {stopwatch.ElapsedMilliseconds} мс\n");

            return RedirectToAction("Dashboard");
        }

    }
}
