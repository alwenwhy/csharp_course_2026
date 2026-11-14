using App.Lab3;

namespace App;

public static class Program
{
    public static void Main()
    {
        var ivan = UserCreator.CreateUser("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", "+79131234567");
        var maria = UserCreator.CreateUser("masha", "Pa$$w0rd", "Мария", "Сидорова", "770987654321", "8 900 111-22-33");
        
        Console.WriteLine(ivan.GetUserInfo());
        Console.WriteLine(maria.GetUserInfo());
        Console.WriteLine($"Хеш пароля Марии: {maria.PasswordHash}");

        var oleg = new User
        {
            Login = "oleg_smirnov",
            Name = "oleg",
            Surname = "smirnov",
            Inn = "4543646463",
            Phone = "+79131234567"
        };
        Console.WriteLine(oleg.GetUserInfo());
        
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
}