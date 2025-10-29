using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class SilvicultureEvent
{
    public int EventId { get; set; }

    public int PlotId { get; set; }

    public int EventTypeId { get; set; }

    public string Description { get; set; } = null!;

    public int? TreesNumber { get; set; }

    public DateOnly Date { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public virtual ForestPlot Plot { get; set; } = null!;
}
