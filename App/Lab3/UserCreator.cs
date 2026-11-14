using System.Security.Cryptography;
using System.Text;

namespace App.Lab3;

public static class UserCreator
{
    public static User CreateUser(string login, string password, string name, string surname, string inn, string phone)
    {
        var passwordHash = GetHash(password);
        
        return new User(login, passwordHash, name, surname, inn, phone);
    }

    private static string GetHash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hash = MD5.HashData(passwordBytes);

        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}