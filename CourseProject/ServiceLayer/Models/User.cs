using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<ForestPlot> ForestPlots { get; set; } = new List<ForestPlot>();

    public virtual Role Role { get; set; } = null!;
}
