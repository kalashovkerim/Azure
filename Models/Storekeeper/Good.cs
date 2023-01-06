using System;
using System.Collections.Generic;

namespace NimbProjectApp.Models.Storekeeper;

public partial class Good
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public DateTime? DateAdd { get; set; }

    public DateTime? DateSold { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
