namespace Marketplace.App;

using Marketplace.Core;

public static class Program
{
    public static void Main()
    {
        var ivan = UserCreator.CreateUser("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", "+79131234567");
        Console.WriteLine(ivan.GetUserInfo());
        
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Ноутбук", 80000, ProductCategory.Electronics));
        cart.AddProduct(new Product("Куртка", 10000, ProductCategory.Clothing));
        cart.AddProduct(new Product("Хлеб", 60, ProductCategory.Food));
        cart.AddProduct(new Product("Детский планшет", 12000, ProductCategory.Electronics | ProductCategory.Toys));
        cart.AddProduct(new Product("Конструктор", 3000, ProductCategory.Toys));
        
        Console.WriteLine("Корзина:");
        for (var i = 0; i < cart.Count; i++)
            Console.WriteLine($"  {cart[i].Name} — {cart[i].Price} ({cart[i].Categories})");

        Console.WriteLine($"Итого со скидками по категориям: {cart.TotalPrice}");
        
        // cart.SetDiscount(5000); 
        MarketingService.ApplySecretDiscount(cart, 5);
        
        Console.WriteLine($"Итого со скидками по категориям + секретной: {cart.TotalPrice}");
        
        var products = cart.GetAllProducts();
        products[0] = new Product("Подделка", 1, ProductCategory.None);
        Console.WriteLine($"В массиве: {products[0].Name}, в корзине: {cart[0].Name}");
        
        cart.Clear();
        Console.WriteLine($"Продуктов в корзине после очистки: {cart.Count}");
    }
}