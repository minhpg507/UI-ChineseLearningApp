using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class Flashcard
{
    public int Id { get; set; }

    public int? WordListId { get; set; }

    public string FrontText { get; set; } = null!;

    public string? Pinyin { get; set; }

    public string BackText { get; set; } = null!;

    public string? WordType { get; set; }

    public string? Example { get; set; }

    public string? Note { get; set; }

    public string? ImageUrl { get; set; }

    public virtual WordList? WordList { get; set; }
}
