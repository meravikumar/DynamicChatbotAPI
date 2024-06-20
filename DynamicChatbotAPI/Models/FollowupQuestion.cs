using System;
using System.Collections.Generic;

namespace DynamicChatbotAPI.Models;

public partial class FollowupQuestion
{
    public int QuestionId { get; set; }

    public int? ActionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public int SequenceOrder { get; set; }

    public virtual Action? Action { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();
}
