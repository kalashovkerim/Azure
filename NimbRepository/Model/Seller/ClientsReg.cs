using System;
using System.Collections.Generic;
using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Model.Seller;

public partial class ClientsReg
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int GoodsId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Good Goods { get; set; } = null!;
}
