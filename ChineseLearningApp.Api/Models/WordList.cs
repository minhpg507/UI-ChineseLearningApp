using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class WordList
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}
