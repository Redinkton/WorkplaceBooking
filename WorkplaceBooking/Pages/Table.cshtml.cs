using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkplaceBooking.Services;
using System.Security.Claims;

[Authorize]
public class TableModel : PageModel
{
    private readonly SeatService _seatService;
    public IReadOnlyDictionary<int, string> OccupiedSeats { get; set; }

    public TableModel(SeatService seatService)
    {
        _seatService = seatService;
    }

    public async Task<IActionResult> OnPostReserve(int seatNumber)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            TempData["Error"] = "Ошибка: Пользователь не найден";
            return RedirectToPage();
        }

        if (!await _seatService.ReserveSeatAsync(seatNumber, userId))
        {
            TempData["Error"] = "Ìåñòî óæå çàíÿòî";
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFree(int seatNumber)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        if (!await _seatService.FreeSeatAsync(seatNumber, userId))
        {
            TempData["Error"] = "Вы не можете освободить это место";
        }
        return RedirectToPage();
    }

    public async Task OnGet()
    {
        OccupiedSeats = await _seatService.GetOccupiedSeatsAsync();
    }
}
