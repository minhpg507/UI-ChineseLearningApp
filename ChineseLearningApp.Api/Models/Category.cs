using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IconUrl { get; set; }

    public int? TotalExercises { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
