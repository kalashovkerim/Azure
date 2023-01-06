using System;
using System.Collections.Generic;

namespace NimbProjectApp.Models.Storekeeper;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public DateTime? Date { get; set; }

    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
