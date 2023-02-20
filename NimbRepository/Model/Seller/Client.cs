

namespace NimbRepository.Model.Seller;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PatronymicName { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;
}
