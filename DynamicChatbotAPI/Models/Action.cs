using System;
using System.Collections.Generic;

namespace DynamicChatbotAPI.Models;

public partial class Action
{
    public int ActionId { get; set; }

    public int? CompanyId { get; set; }

    public string ActionName { get; set; } = null!;

    public string? ApiUrl { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<FollowupQuestion> FollowupQuestions { get; set; } = new List<FollowupQuestion>();
}
