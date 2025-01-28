using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using WorkplaceBooking;

public class TableModel : PageModel
{
    private readonly SeatService _seatService;

    public TableModel(SeatService seatService)
    {
        _seatService = seatService;
    }

    // ������ ������� ���� ��� ����������� �� ��������
    public List<int> OccupiedSeats => _seatService.OccupiedSeats;

    public void OnGet()
    {
        // ������ �� ������, ������ ���������� ��������
    }

    // ���������� ��� ������������ �����
    public IActionResult OnPostReserve(int seatNumber)
    {
        if (_seatService.ReserveSeat(seatNumber))
        {
            return new JsonResult(new { success = true });
        }
        else
        {
            return new JsonResult(new { success = false, message = "����� ��� ������!" });
        }
    }
}