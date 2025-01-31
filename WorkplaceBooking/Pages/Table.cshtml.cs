using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WorkplaceBooking;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class TableModel : PageModel
{
    private readonly SeatService _seatService;
    public IReadOnlyDictionary<int, string> OccupiedSeats => _seatService.OccupiedSeats;

    public TableModel(SeatService seatService)
    {
        _seatService = seatService;
    }

    public IActionResult OnPostReserve(int seatNumber)
    {
        if (!_seatService.ReserveSeat(seatNumber, User.Identity.Name))
        {
            TempData["Error"] = "����� ��� ������";
        }

        return RedirectToPage();
    }

    public IActionResult OnPostFree(int seatNumber)
    {
        if (!_seatService.ReserveSeat(seatNumber, User.Identity.Name))
        {
            TempData["Error"] = "�� �� ������ ���������� ��� �����";
        }

        return RedirectToPage();
    }
    public void OnGet() { }
}