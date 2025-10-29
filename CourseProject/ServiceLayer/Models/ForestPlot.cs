using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class ForestPlot
{
    public int PlotId { get; set; }

    public int UserId { get; set; }

    public byte Compartment { get; set; }

    public byte Subcompartment { get; set; }

    public virtual ICollection<SilvicultureEvent> SilvicultureEvents { get; set; } = new List<SilvicultureEvent>();

    public virtual ICollection<TreesNumber> TreesNumbers { get; set; } = new List<TreesNumber>();

    public virtual User User { get; set; } = null!;
}
