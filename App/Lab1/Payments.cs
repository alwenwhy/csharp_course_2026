namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        decimal total;
        var monthlyRate = rate / (12m * 100m);

        if (plan == PaymentsPlan.Annuity) {
            if (monthlyRate == 0m)
                return Math.Round(amount, 2);

            var powPart = (decimal)Math.Pow((double)(1 + monthlyRate), (double)monthsCount);
            var monthlyPayment = amount * ((monthlyRate * powPart) / (powPart - 1));
        
            total = monthlyPayment * monthsCount;
        } else {
            var overpayment = (amount * monthlyRate * (monthsCount + 1)) / 2;

            total = amount + overpayment;
        }

        return Math.Round(total, 1);
    }
}