using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class TreesNumber
{
    public int PlotId { get; set; }

    public int TreeTypeId { get; set; }

    public int Amount { get; set; }

    public virtual ForestPlot Plot { get; set; } = null!;

    public virtual TreeType TreeType { get; set; } = null!;
}
