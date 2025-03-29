using Domain.Utils;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : BaseEntity
{
    public User(
        Name? name, 
        string userName,
        Email email, 
        Password password)
    {
        Name = name;
        UserName = userName;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Name Name { get; private set; }
    public string UserName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public ICollection<Contact> Contacts { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}