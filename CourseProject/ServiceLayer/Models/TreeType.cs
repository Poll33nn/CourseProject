using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class TreeType
{
    public int TreeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TreesNumber> TreesNumbers { get; set; } = new List<TreesNumber>();
}
