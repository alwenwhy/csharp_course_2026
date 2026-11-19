using Marketplace.Core;

namespace AppTests.Lab3;

public class ProductTests
{
    [Test]
    public void Constructor_Returns_ProductWithGivenData_When_DataValid()
    {
        var product = new Product("Ноутбук", 80000, ProductCategory.Electronics);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(product.Name, Is.EqualTo("Ноутбук"));
            Assert.That(product.Price, Is.EqualTo(80000m));
            Assert.That(product.Categories, Is.EqualTo(ProductCategory.Electronics));
            Assert.That(product.Id, Is.Not.EqualTo(Guid.Empty));
        }
    }

    [Test]
    public void Constructor_Returns_ProductWithSeveralCategories_When_CategoriesCombined()
    {
        var product = new Product("Детский планшет", 12000, ProductCategory.Electronics | ProductCategory.Toys);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(product.Categories.HasFlag(ProductCategory.Electronics), Is.True);
            Assert.That(product.Categories.HasFlag(ProductCategory.Toys), Is.True);
            Assert.That(product.Categories.HasFlag(ProductCategory.Food), Is.False);
        }
    }

    [Test]
    public void Constructor_Returns_DifferentIds_When_TwoProductsCreated()
    {
        var first = new Product("Хлеб", 60, ProductCategory.Food);
        var second = new Product("Хлеб", 60, ProductCategory.Food);

        Assert.That(first.Id, Is.Not.EqualTo(second.Id));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_Throws_Exception_When_PriceNotPositive(decimal price)
    {
        Assert.Throws<ArgumentException>(() => new Product("Хлеб", price, ProductCategory.Food));
    }

    [TestCase("")]
    [TestCase(null)]
    public void Constructor_Throws_Exception_When_NameEmpty(string? name)
    {
        Assert.Throws<ArgumentException>(() => new Product(name!, 60, ProductCategory.Food));
    }
}