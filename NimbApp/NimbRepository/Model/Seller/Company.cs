using System;
using System.Collections.Generic;

namespace NimbRepository.Model.Seller;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? Date { get; set; }

    public virtual ICollection<CompaniesReg> CompaniesRegs { get; } = new List<CompaniesReg>();
}
