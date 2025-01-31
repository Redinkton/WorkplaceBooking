namespace WorkplaceBooking
{
    public class SeatService
    {
        private readonly Dictionary<int, string> _occupiedSeats = new();
        public IReadOnlyDictionary<int, string> OccupiedSeats => _occupiedSeats;

        public bool ReserveSeat(int seatNumber, string userName)
        {
            if (_occupiedSeats.ContainsKey(seatNumber))
            {
                return false;
            }

            _occupiedSeats[seatNumber] = userName;
            return true;
        }

        public bool FreeSeat(int seatNumber, string userName)
        {
            if (_occupiedSeats.TryGetValue(seatNumber, out var owner) && owner == userName)
            {
                return true;
            }
            return false;
        }
    }
}