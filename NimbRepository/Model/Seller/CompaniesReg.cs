using System;
using System.Collections.Generic;
using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Model.Seller;

public partial class CompaniesReg
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int GoodsId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Good Goods { get; set; } = null!;
}
