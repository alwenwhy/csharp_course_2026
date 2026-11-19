using Marketplace.Core;

namespace AppTests.Lab3;

public class ShoppingCartTests
{
    [Test]
    public void TotalPrice_Returns_Zero_When_CartEmpty()
    {
        var cart = new ShoppingCart();

        Assert.That(cart.TotalPrice, Is.EqualTo(0m));
    }

    [TestCase(ProductCategory.Electronics, 900)]
    [TestCase(ProductCategory.Clothing, 800)]
    [TestCase(ProductCategory.Food, 950)]
    [TestCase(ProductCategory.Books, 850)]
    [TestCase(ProductCategory.Toys, 1000)]
    [TestCase(ProductCategory.None, 1000)]
    public void TotalPrice_Returns_PriceWithCategoryDiscount_When_OneProductAdded(
        ProductCategory category, decimal expected)
    {
        var cart = CreateCart(new Product("Товар", 1000, category));

        Assert.That(cart.TotalPrice, Is.EqualTo(expected));
    }

    [TestCase(ProductCategory.Electronics | ProductCategory.Clothing, 800)]
    [TestCase(ProductCategory.Electronics | ProductCategory.Toys, 900)]
    [TestCase(ProductCategory.Food | ProductCategory.Books, 850)]
    public void TotalPrice_Returns_PriceWithMaxDiscount_When_ProductHasSeveralCategories(
        ProductCategory categories, decimal expected)
    {
        var cart = CreateCart(new Product("Товар", 1000, categories));

        Assert.That(cart.TotalPrice, Is.EqualTo(expected));
    }

    [Test]
    public void TotalPrice_Returns_SumWithDiscounts_When_SeveralProductsAdded()
    {
        var cart = CreateCart(
            new Product("Ноутбук", 80000, ProductCategory.Electronics),
            new Product("Куртка", 10000, ProductCategory.Clothing),
            new Product("Хлеб", 60, ProductCategory.Food),
            new Product("Конструктор", 3000, ProductCategory.Toys));

        // 72000 + 8000 + 57 + 3000
        Assert.That(cart.TotalPrice, Is.EqualTo(83057m));
    }

    [Test]
    public void TotalPrice_Returns_RoundedValue_When_DiscountGivesFraction()
    {
        var cart = CreateCart(new Product("Жвачка", 9.99m, ProductCategory.Food));

        // 9.99 * 0.95 = 9.4905
        Assert.That(cart.TotalPrice, Is.EqualTo(9.49m));
    }

    [Test]
    public void Indexer_Returns_Products_When_IndexValid()
    {
        var laptop = new Product("Ноутбук", 80000, ProductCategory.Electronics);
        var bread = new Product("Хлеб", 60, ProductCategory.Food);
        var cart = CreateCart(laptop, bread);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(cart[0], Is.SameAs(laptop));
            Assert.That(cart[1], Is.SameAs(bread));
        }
    }

    [TestCase(-1)]
    [TestCase(1)]
    [TestCase(100)]
    public void Indexer_Throws_Exception_When_IndexOutOfRange(int index)
    {
        var cart = CreateCart(new Product("Хлеб", 60, ProductCategory.Food));

        Assert.Throws<IndexOutOfRangeException>(() => _ = cart[index]);
    }

    [Test]
    public void Count_Returns_NumberOfProducts_When_ProductsAdded()
    {
        var cart = CreateCart(
            new Product("Хлеб", 60, ProductCategory.Food),
            new Product("Хлеб", 60, ProductCategory.Food),
            new Product("Куртка", 10000, ProductCategory.Clothing));

        Assert.That(cart.Count, Is.EqualTo(3));
    }

    [Test]
    public void Count_Returns_Zero_When_CartCleared()
    {
        var cart = CreateCart(new Product("Хлеб", 60, ProductCategory.Food));

        cart.Clear();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(cart.Count, Is.EqualTo(0));
            Assert.That(cart.TotalPrice, Is.EqualTo(0m));
        }
    }

    [Test]
    public void AddProduct_Throws_Exception_When_ProductNull()
    {
        var cart = new ShoppingCart();

        Assert.Throws<ArgumentException>(() => cart.AddProduct(null!));
    }

    [Test]
    public void GetAllProducts_Returns_AllProducts_When_ProductsAdded()
    {
        var laptop = new Product("Ноутбук", 80000, ProductCategory.Electronics);
        var bread = new Product("Хлеб", 60, ProductCategory.Food);
        var cart = CreateCart(laptop, bread);

        Assert.That(cart.GetAllProducts(), Is.EqualTo(new[] { laptop, bread }));
    }

    [Test]
    public void GetAllProducts_Returns_EmptyArray_When_CartEmpty()
    {
        var cart = new ShoppingCart();

        Assert.That(cart.GetAllProducts(), Is.Empty);
    }

    [Test]
    public void GetAllProducts_Returns_Copy_When_ReturnedArrayChanged()
    {
        var laptop = new Product("Ноутбук", 80000, ProductCategory.Electronics);
        var cart = CreateCart(laptop);

        var products = cart.GetAllProducts();
        products[0] = new Product("Подделка", 1, ProductCategory.None);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(cart[0], Is.SameAs(laptop));
            Assert.That(cart.TotalPrice, Is.EqualTo(72000m));
        }
    }

    [Test]
    public void GetAllProducts_Returns_NewArray_When_CalledTwice()
    {
        var cart = CreateCart(new Product("Хлеб", 60, ProductCategory.Food));

        Assert.That(cart.GetAllProducts(), Is.Not.SameAs(cart.GetAllProducts()));
    }
    
    private static ShoppingCart CreateCart(params Product[] products)
    {
        var cart = new ShoppingCart();

        foreach (var product in products)
            cart.AddProduct(product);

        return cart;
    }
}