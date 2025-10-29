using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SilvicultureEvent> SilvicultureEvents { get; set; } = new List<SilvicultureEvent>();
}
