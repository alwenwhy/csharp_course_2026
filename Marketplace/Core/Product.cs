namespace Marketplace.Core;

public class Product
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductCategory Categories { get; }

    public Product(string name, decimal price, ProductCategory categories)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Product name cannot be null or empty");
        
        if (price <= 0)
            throw new ArgumentException("Product price cannot be zero or negative");
        
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Categories = categories;
    }
}