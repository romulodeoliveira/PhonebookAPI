using Flunt.Validations;

namespace Domain.ValueObjects;

public class Email : BaseValueObject
{
    public Email(
        string address)
    {
        Address = address;
        
        AddNotifications(
            new Contract<Email>()
                .Requires()
                .IsNotNullOrEmpty(Address, "Address", "O endereço de e-mail é obrigatório.")
                .IsEmail(Address, "Address", "O endereço de e-mail precisa ser válido.")
        );
    }
    
    public string Address { get; private set; }
}