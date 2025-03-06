using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdateUserNoteDto
    {

        public string? Diagnosis { get; set; }
        public string? Note { get; set; }
        public string? Detail { get; set; }
    }
}