namespace WorkplaceBooking
{
    public class SeatService
    {
        public List<int> OccupiedSeats { get; } = new List<int> { 2, 4, 6 }; // Пример начальных данных

        // Метод для бронирования места
        public bool ReserveSeat(int seatNumber)
        {
            if (OccupiedSeats.Contains(seatNumber))
            {
                return false; // Место уже занято
            }

            OccupiedSeats.Add(seatNumber);
            return true; // Место успешно забронировано
        }
    }
}