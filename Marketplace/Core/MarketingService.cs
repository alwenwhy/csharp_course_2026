namespace Marketplace.Core;

public static class MarketingService
{
    public static void ApplySecretDiscount(ShoppingCart cart, decimal amount)
    {
        if (cart == null)
            throw new ArgumentException("Cart cannot be null");
        
        cart.SetDiscount(amount);
    }
}