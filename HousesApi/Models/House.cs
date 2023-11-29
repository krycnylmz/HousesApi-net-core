using System;
using System.ComponentModel.DataAnnotations;

namespace HousesApi.Models
{
    public class House
    {
        public long Id { get; set; }
        public long user_id { get; set; }
        [Required]
        public string house_name { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public bool garden_view { get; set; }
        public bool wifi { get; set; }
        public bool lake_view { get; set; }
        public bool pool { get; set; }
        public bool lake_access { get; set; }
        public bool hot_tub { get; set; }
        public DateTime created_at { get; set; }
    }
}
