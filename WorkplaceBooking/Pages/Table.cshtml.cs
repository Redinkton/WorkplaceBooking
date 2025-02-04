using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WorkplaceBooking.Services;


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
        if (!await _seatService.ReserveSeatAsync(seatNumber, User.Identity.Name))
        {
            TempData["Error"] = "Место уже занято";
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFree(int seatNumber)
    {
        if (!await _seatService.FreeSeatAsync(seatNumber, User.Identity.Name))
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
