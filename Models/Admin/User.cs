using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NimbApp.Models.Admin;

public partial class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Surname { get; set; } = null!;
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Number { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public string Position { get; set; } = null!;
}
