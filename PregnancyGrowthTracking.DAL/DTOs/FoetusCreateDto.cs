using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class FoetusCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(30, ErrorMessage = "Name must be at most 30 characters.")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Name must only contain letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression(@"^(Nam|Nữ)$", ErrorMessage = "Gender must be either 'Nam' or 'Nữ'.")]
        public string Gender { get; set; }

    }
}
