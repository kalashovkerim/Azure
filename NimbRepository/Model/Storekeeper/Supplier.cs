
namespace NimbRepository.Model.Storekeeper;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Good> Goods { get; } = new List<Good>();
}
