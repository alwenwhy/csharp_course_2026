namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        if (isShorNotation)
            return isFirstCapital ? "Руб." : "руб.";

        //надеюсь я правильно понял, что от меня хотят
        var output = (Math.Abs(price) % 10, Math.Abs(price) % 100) switch {
            (_, >= 11 and <= 14) => "рублей",
            (1, _) => "рубль",
            (>= 2 and <= 4, _) => "рубля",
            _ => "рублей"
        };

        return isFirstCapital ? "Р" + output[1..] : output;
    }
}