using Domain.ValueObjects;
using Flunt.Validations;

namespace Domain.Entities;

public class Contact : BaseEntity
{
    public Contact(
        Name name, 
        string phone, 
        Guid userId, 
        User user)
    {
        Name = name;
        Phone = phone;
        UserId = userId;
        User = user;
        CreatedAt = DateTime.UtcNow;

        AddNotifications(
            new Contract<Contact>()
                .Requires()
                .IsNotNullOrEmpty(Phone, "Phone", "O telefone é obrigatório.")
                .IsTrue(Phone.Length == 11, "Phone", "O telefone deve conter 11 caracteres.")
                .Matches(Phone, @"^[0-9\s\-\(\)]+$", "Phone", "O telefone contém caracteres inválidos.")
        );

        AddNotifications(Name);
    }
    
    public Name Name { get; private set; }
    public string Phone { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public void UpdatePhoneNumber(string newPhone)
    {
        Phone = newPhone;
        UpdatedAt = DateTime.UtcNow;
        
        AddNotifications(
            new Contract<Contact>()
                .Requires()
                .IsNotNullOrEmpty(newPhone, "Phone", "A número de telefone não pode estar vazio.")
                .IsTrue(Phone.Length == 11, "Phone", "O telefone deve conter 11 caracteres.")
                .Matches(Phone, @"^[0-9\s\-\(\)]+$", "Phone", "O telefone contém caracteres inválidos.")
        );
    }
    
    public void UpdateName(Name newName)
    {
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
        
        AddNotifications(Name);
    }
}