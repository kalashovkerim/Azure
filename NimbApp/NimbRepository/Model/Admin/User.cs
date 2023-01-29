using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NimbRepository.Model.Admin;

public partial class User : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
 
    public string PatronymicName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string Position { get; set; } = null!;
    public DateTime? Status { get; set; }
}
