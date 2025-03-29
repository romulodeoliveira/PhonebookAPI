using System.Security.Cryptography;
using Flunt.Validations;

namespace Domain.ValueObjects;

public class Password : BaseValueObject
{
    public Password(
        string value)
    {
        Value = value;
        
        AddNotifications(
            new Contract<Password>()
                .Requires()
                .IsNotNullOrEmpty(Value, "Value", "A senha é obrigatória.")
                .IsTrue(StrongPassword(Value), "Value", "A senha precisa conter entre 6 e 12 caracteres com letras maiúsculas, minúsculas, números e caracteres especiais.")
        );
    }

    public string Value { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    
    private bool StrongPassword(string password)
    {
        if (password.Length < 6 || password.Length > 12)
        {
            return false;
        }

        if (!password.Any(c => char.IsDigit(c)))
        {
            return false;
        }

        if (!password.Any(c => char.IsUpper(c)))
        {
            return false;
        }

        if (!password.Any(c => char.IsLower(c)))
        {
            return false;
        }

        if (!password.Any(c => char.IsSymbol(c)))
        {
            return false;
        }

        var repeatedCounter = 0;
        var lastCharacter = '\0';

        foreach (var c in password)
        {
            if (c == repeatedCounter)
            {
                repeatedCounter++;
            }
            else
            {
                repeatedCounter = 0;
            }

            if (repeatedCounter == 2)
            {
                return false;
            }

            lastCharacter = c;
        }

        return true;
    }
}