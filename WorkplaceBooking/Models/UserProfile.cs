using System.ComponentModel.DataAnnotations;

namespace WorkplaceBooking.Models
{
    public class UserProfile
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }
    }
}
