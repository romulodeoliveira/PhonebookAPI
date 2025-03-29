using Flunt.Validations;

namespace Domain.ValueObjects;

public class Name : BaseValueObject
{
    public Name(
        string firstName, 
        string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        
        AddNotifications(
            new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "FirstName", "O primeiro nome é obrigatório.")
                .IsNotNullOrEmpty(LastName, "LastName", "O último nome é obrigatório.")
            );
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}