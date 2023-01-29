using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NimbRepository.Model.Seller;

public partial class Client : IEntity
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string PatronymicName { get; set; } = null!;
    [Required]
    public string Number { get; set; } = null!;
    [Required]
    public string EmailAddress { get; set; } = null!;

    public DateTime? Date { get; set; } = DateTime.Now;

    public virtual ICollection<ClientsReg> ClientsRegs { get; } = new List<ClientsReg>();
}
