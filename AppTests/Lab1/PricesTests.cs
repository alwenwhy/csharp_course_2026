using App.Lab1;

namespace AppTests.Lab1;

public class PricesTests
{
    [TestCase(1000052, "рубля")]
    [TestCase(25, "рублей")]
    [TestCase(101, "рубль")]
    [TestCase(10, "рублей")]
    [TestCase(122, "рубля")]
    public void GetCurrencyAlias_LongNotation_LowerCase(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, false);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(1, "Рубль")]
    [TestCase(2, "Рубля")]
    [TestCase(5, "Рублей")]
    [TestCase(11, "Рублей")]
    [TestCase(21, "Рубль")]
    public void GetCurrencyAlias_LongNotation_UpperCase(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, false, true);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(1, "руб.")]
    [TestCase(2, "руб.")]
    [TestCase(5, "руб.")]
    [TestCase(11, "руб.")]
    [TestCase(21, "руб.")]
    public void GetCurrencyAlias_ShortNotation_LowerCase(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, false);
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [TestCase(1, "Руб.")]
    [TestCase(2, "Руб.")]
    [TestCase(5, "Руб.")]
    [TestCase(11, "Руб.")]
    [TestCase(21, "Руб.")]
    public void GetCurrencyAlias_ShortNotation_UpperCase(int price, string expected)
    {
        var actual = Prices.GetCurrencyAlias(price, true, true);
        Assert.That(actual, Is.EqualTo(expected));
    }
}