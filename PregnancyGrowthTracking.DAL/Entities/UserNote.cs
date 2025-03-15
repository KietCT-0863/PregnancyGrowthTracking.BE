using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class UserNote
{
    public int NoteId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Diagnosis { get; set; }

    public string? Note { get; set; }

    public string? Detail { get; set; }

    public string? UserNotePhoto { get; set; }
}
