using App.Lab3;

namespace App;

public static class Program
{
    public static void Main()
    {
        var ivan = new User("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", "+79131234567");
        var maria = new User("masha", "Pa$$w0rd", "Мария", "Сидорова", "770987654321", "89001112233");
        
        Console.WriteLine(ivan.GetUserFullName());
        Console.WriteLine(maria.GetUserFullName());
        
        Console.WriteLine($"Новый номер +79990001122: {ivan.TryUpdatePhone("+79990001122")}");
        Console.WriteLine($"Новый номер 12345: {ivan.TryUpdatePhone("12345")}");
    }
}