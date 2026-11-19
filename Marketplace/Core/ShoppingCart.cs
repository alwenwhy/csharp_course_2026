namespace Marketplace.Core;

public class ShoppingCart
{
    private static readonly Dictionary<ProductCategory, decimal> Discounts = new()
    {
        [ProductCategory.Electronics] = 10m,
        [ProductCategory.Clothing] = 20m,
        [ProductCategory.Food] = 5m,
        [ProductCategory.Books] = 15m
    };
    
    private readonly List<Product> products = new();
    private decimal discount = 0m;
    
    public int Count => products.Count;

    public Product this[int index]
    {
        get
        {
            if (index < 0 || index >= products.Count)
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            
            return products[index];
        }
    }

    public decimal TotalPrice
    {
        get
        {
            var total = 0m;

            foreach (var product in products)
            {
                var discountPercent = GetCategoryDiscount(product.Categories);
                total += product.Price * (100 - discountPercent) / 100;
            }

            return Math.Round(total * (100 - discount) / 100, 2);
        }
    }

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentException("Product cannot be null");
        
        products.Add(product);
    }

    public void Clear()
    {
        products.Clear();
        discount = 0m;
    }

    public Product[] GetAllProducts()
    {
        var result = new Product[products.Count];
        
        for (var i =  0; i < products.Count; i++)
            result[i] = products[i];
        
        return result;
    }

    internal void SetDiscount(decimal newDiscount)
    {
        if (newDiscount is < 0 or > 100)
            throw new ArgumentException("Discount must be between 0 and 100");
        
        discount = newDiscount;
    }
    
    private static decimal GetCategoryDiscount(ProductCategory categories)
    {
        var percent = 0m;

        foreach (var pair in Discounts)
        {
            // на всякий случай
            if (pair.Key == ProductCategory.None)
                continue;
            
            if (categories.HasFlag(pair.Key) && pair.Value > percent)
                percent = pair.Value;
        }

        return percent;
    }
}