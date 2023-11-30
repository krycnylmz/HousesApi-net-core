using System;
using System.ComponentModel.DataAnnotations;

namespace HousesApi.Models
{
    public class Booking
    {
        public long Id { get; set; }
        public long house_id { get; set; }
        public long user_id { get; set; }

        [Required]
        public int number_of_people { get; set; }

        [Required]
        public DateTime check_in { get; set; }

        [Required]
        public DateTime check_out { get; set; }

        public DateTime created_at { get; set; }
    }
}
