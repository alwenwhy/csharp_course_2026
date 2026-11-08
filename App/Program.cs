using App.Lab3;

namespace App;

public static class Program
{
    public static void Main()
    {
        var ivan = UserCreator.CreateUser("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", "+79131234567");
        var maria = UserCreator.CreateUser("masha", "Pa$$w0rd", "Мария", "Сидорова", "770987654321", "89001112233");
        
        PrintUser(ivan);
        PrintUser(maria);

        var oleg = new User
        {
            Login = "oleg_smirnov",
            Name = "oleg",
            Surname = "smirnov",
            Inn = "4543646463",
            Phone = "+79131234567"
        };
        PrintUser(oleg);
        
        oleg.Phone = "+79989562441";
        Console.WriteLine(oleg.Phone);

        try
        {
            oleg.Phone = "123";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        //id поменять нельзя, init-свойство задаётся только в конструкторе или инициализаторе.
    }

    private static void PrintUser(User user)
    {
        Console.WriteLine(
            $"{user.GetUserFullName()} ({user.Login}), тел. {user.Phone}, " +
            $"id {user.Id}, зарегистрирован {user.RegisterDate:yyyy-MM-dd HH:mm} UTC, " +
            $"хеш пароля {user.PasswordHash ?? "-"}");
    }
}