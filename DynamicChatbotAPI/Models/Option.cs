using System;
using System.Collections.Generic;

namespace DynamicChatbotAPI.Models;

public partial class Option
{
    public int OptionId { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public virtual FollowupQuestion? Question { get; set; }
}
