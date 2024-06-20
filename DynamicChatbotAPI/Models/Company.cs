using System;
using System.Collections.Generic;

namespace DynamicChatbotAPI.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();
}
