using System;
using System.Collections.Generic;

namespace ChineseLearningApp.Api.Models;

public partial class Word
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Hanzi { get; set; } = null!;

    public string Pinyin { get; set; } = null!;

    public int ExpectedOrder { get; set; }

    public virtual Question Question { get; set; } = null!;
}
