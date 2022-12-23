using System;
using System.Collections.Generic;

namespace NimbApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Position { get; set; } = null!;
}
