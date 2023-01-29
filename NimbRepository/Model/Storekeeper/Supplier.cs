using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NimbRepository.Model.Storekeeper;

public partial class Supplier : IEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Number { get; set; } = null!;
    [Required]
    public string EmailAddress { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    public DateTime? Date { get; set; }

    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
