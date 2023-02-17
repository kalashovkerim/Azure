using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NimbRepository.Model.Seller;

namespace NimbRepository.Model.Storekeeper;

public partial class Good
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Category { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int Count { get; set; }

    public decimal? Price { get; set; }

    public int SupplierId { get; set; }

    public DateTime? DateAdd { get; set; } = DateTime.Now;

    public DateTime? DateSold { get; set; }

    public virtual ICollection<ClientsNotReg> ClientsNotRegs { get; } = new List<ClientsNotReg>();

    public virtual ICollection<ClientsReg> ClientsRegs { get; } = new List<ClientsReg>();

    public virtual ICollection<CompaniesReg> CompaniesRegs { get; } = new List<CompaniesReg>();

    public virtual Supplier Supplier { get; set; } = null!;
}
