using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Drug
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; }
    public Status Status { get; set; }
    public DateTime ExpirationDateTime { get; set; }
}