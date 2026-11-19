using Marketplace.Core;

namespace AppTests.Lab3;

public class MarketingServiceTests
{
    [TestCase(10, 64800)]
    [TestCase(0, 72000)]
    [TestCase(100, 0)]
    public void TotalPrice_Returns_PriceWithSecretDiscount_When_DiscountApplied(decimal discount, decimal expected)
    {
        var cart = CreateCartWithLaptop();

        MarketingService.ApplySecretDiscount(cart, discount);

        Assert.That(cart.TotalPrice, Is.EqualTo(expected));
    }

    [Test]
    public void TotalPrice_Returns_PriceWithLastDiscount_When_DiscountAppliedTwice()
    {
        var cart = CreateCartWithLaptop();

        MarketingService.ApplySecretDiscount(cart, 10);
        MarketingService.ApplySecretDiscount(cart, 5);

        Assert.That(cart.TotalPrice, Is.EqualTo(68400m));
    }

    [Test]
    public void TotalPrice_Returns_RoundedValue_When_BothDiscountsGiveFraction()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Жвачка", 9.99m, ProductCategory.Food));

        MarketingService.ApplySecretDiscount(cart, 10);

        Assert.That(cart.TotalPrice, Is.EqualTo(8.54m));
    }

    [Test]
    public void TotalPrice_Returns_PriceWithoutSecretDiscount_When_CartClearedAndRefilled()
    {
        var cart = CreateCartWithLaptop();
        MarketingService.ApplySecretDiscount(cart, 10);

        cart.Clear();
        cart.AddProduct(new Product("Ноутбук", 80000, ProductCategory.Electronics));

        Assert.That(cart.TotalPrice, Is.EqualTo(72000m));
    }

    [TestCase(-1)]
    [TestCase(101)]
    public void ApplySecretDiscount_Throws_Exception_When_DiscountOutOfRange(decimal discount)
    {
        var cart = CreateCartWithLaptop();

        Assert.Throws<ArgumentException>(() => MarketingService.ApplySecretDiscount(cart, discount));
    }

    [Test]
    public void TotalPrice_Returns_PriceWithoutSecretDiscount_When_InvalidDiscountRejected()
    {
        var cart = CreateCartWithLaptop();

        MarketingService.ApplySecretDiscount(cart, 10);

        Assert.Throws<ArgumentException>(() => MarketingService.ApplySecretDiscount(cart, 150));
        Assert.That(cart.TotalPrice, Is.EqualTo(64800m));
    }

    [Test]
    public void ApplySecretDiscount_Throws_Exception_When_CartNull()
    {
        Assert.Throws<ArgumentException>(() => MarketingService.ApplySecretDiscount(null!, 10));
    }
    
    private static ShoppingCart CreateCartWithLaptop()
    {
        var cart = new ShoppingCart();
        cart.AddProduct(new Product("Ноутбук", 80000, ProductCategory.Electronics)); // 72000 со скидкой на электрогику

        return cart;
    }
}