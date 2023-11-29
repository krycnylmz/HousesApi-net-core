using System;
using System.ComponentModel.DataAnnotations;

namespace HousesApi.Models
{
	public class User
	{
		public long Id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    
	}
}


