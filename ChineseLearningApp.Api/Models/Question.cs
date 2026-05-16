using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class Question
{
    public int Id { get; set; }

    public string VietnameseMeaning { get; set; } = null!;

    public int CategoryId { get; set; }

    public int LevelId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
