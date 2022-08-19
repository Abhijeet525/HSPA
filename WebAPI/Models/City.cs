using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a Mandatory Field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only Numerics are NOT Allowed")]
        public string Name { get; set; }
        
        [Required]
        public string Country { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
    }
}
