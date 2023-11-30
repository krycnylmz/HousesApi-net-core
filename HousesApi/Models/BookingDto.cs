using System;
using System.ComponentModel.DataAnnotations;
namespace HousesApi.Models
{
	public class BookingDto
	{
            [Required]
            public long house_id { get; set; }

            [Required]
            public long user_id { get; set; }

            [Required]
            public int number_of_people { get; set; }

            [Required]
            public DateTime check_in { get; set; }

            [Required]
            public DateTime check_out { get; set; }
        
	}
}

