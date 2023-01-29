using System;
using System.Collections.Generic;

namespace NimbRepository.Model.Finance;

public partial class Wallet : IEntity
{
    public int Id { get; set; }

    public decimal? TotalSumSeller { get; set; }

    public decimal? TotalSumStorekeeper { get; set; }
}
