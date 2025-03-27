using System.ComponentModel.DataAnnotations;

namespace WorkplaceBooking.Models
{
    public class Seat
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        public UserProfile? UserProfile { get; set; }
    }
}