namespace App.Lab1;

public static class Prices
{
    public static string GetCurrencyAlias(int price, bool isShorNotation, bool isFirstCapital)
    {
        if (isShorNotation)
            return isFirstCapital ? "Руб." : "руб.";

        var lastOne = price % 10;
        var lastTwo = price % 100;

        var output = "рублей";
        if (lastOne == 1 && lastTwo != 11)
            output = "рубль";
        else if (lastOne is >= 2 and <= 4 && lastTwo is < 11 or > 14)
            output = "рубля";

        return isFirstCapital ? "Р" + output[1..] : output;
    }
}