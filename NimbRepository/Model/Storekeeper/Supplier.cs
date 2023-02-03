using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NimbRepository.Model.Storekeeper;

public partial class Supplier : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;

    public string Address { get; set; } = null!;
    public DateTime? Date { get; set; }

    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
