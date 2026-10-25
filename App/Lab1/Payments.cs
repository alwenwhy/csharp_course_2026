namespace App.Lab1;

public enum PaymentsPlan
{
    Differentiated,
    Annuity
}

public static class Payments
{
    private const int MonthsInYear = 12;

    public static decimal CalculateTotalPayments(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount)
    {
        var monthlyRate = rate / (MonthsInYear * 100m);
        var total = plan switch {
            PaymentsPlan.Annuity => CalculateAnnuity(amount, monthlyRate, monthsCount),
            PaymentsPlan.Differentiated => CalculateDifferentiated(amount, monthlyRate, monthsCount),
            _ => throw new ArgumentOutOfRangeException(nameof(plan), plan, null)
        };

        return Math.Round(total, 1);
    }

    private static decimal CalculateAnnuity(decimal amount, decimal monthlyRate, int monthsCount)
    {
        if (monthlyRate == 0m)
            return Math.Round(amount, 1);

        var powPart = Pow(1 + monthlyRate, monthsCount);
        var monthlyPayment = amount * ((monthlyRate * powPart) / (powPart - 1));
        
        return monthlyPayment * monthsCount;
    }

    private static decimal CalculateDifferentiated(decimal amount, decimal monthlyRate, int monthsCount)
    {
        var overpayment = (amount * monthlyRate * (monthsCount + 1)) / 2;
        return amount + overpayment;
    }
    
    private static decimal Pow(decimal baseValue, int exp)
    {
        switch (exp) {
            case 0:
                return 1m;
            case < 0:
                throw new ArgumentException("exp must not be negative", nameof(exp));
        }

        var result = 1m;
        var currentBase = baseValue;

        while (exp > 0) {
            if ((exp & 1) == 1)
                result *= currentBase;
            
            currentBase *= currentBase;
            exp >>= 1;
        }

        return result;
    }
}