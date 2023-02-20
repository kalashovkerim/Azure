
using NimbRepository.Model.Seller;

namespace NimbRepository.Model.Storekeeper;

public partial class Good
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal Rate { get; set; }

    public int SupplierId { get; set; }

    public DateTime DateAdd { get; set; } = DateTime.Now;

    public virtual ICollection<GoodsSold> GoodsSolds { get; } = new List<GoodsSold>();

    public virtual Supplier Supplier { get; set; } = null!;
}
