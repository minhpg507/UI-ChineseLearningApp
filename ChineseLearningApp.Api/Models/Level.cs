using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class Level
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
