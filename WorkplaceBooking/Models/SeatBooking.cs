using System.ComponentModel.DataAnnotations;

namespace WorkplaceBooking.Models
{
    public class SeatBooking
    {
        [Key]
        public int SeatNumber { get; set; }
        [Required]
        public string UserName
        {
            get; set;
        }
    }
}
