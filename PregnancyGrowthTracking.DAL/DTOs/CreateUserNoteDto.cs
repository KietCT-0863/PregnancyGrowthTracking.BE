using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreateUserNoteDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0.")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CreateUserNoteDto), "ValidateDateNotInFuture")]
        public DateOnly? Date { get; set; }

        public string? Diagnosis { get; set; }
        public string? Note { get; set; }
        public string? Detail { get; set; }
        public List<IFormFile>? Files { get; set; } 

        public static ValidationResult ValidateDateNotInFuture(DateOnly? date, ValidationContext context)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("Date cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}