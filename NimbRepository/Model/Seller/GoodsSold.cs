
using NimbRepository.Model.Storekeeper;

namespace NimbRepository.Model.Seller;

public partial class GoodsSold
{
    public int Id { get; set; }

    public int GoodsId { get; set; }

    public int Count { get; set; }

    public DateTime DateSold { get; set; } = DateTime.Now;

    public virtual Good Goods { get; set; } = null!;
}
