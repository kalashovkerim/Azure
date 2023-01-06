using System;
using System.Collections.Generic;

namespace NimbProjectApp.Models.Seller;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PatronymicName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public DateTime? Date { get; set; }
}
