using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eateries.Domain.Common;
using Eateries.Domain.Enums;

namespace Eateries.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public List<Order> Orders { get; set; }
    
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }
}