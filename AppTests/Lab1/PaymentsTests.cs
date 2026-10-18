using App.Lab1;

namespace AppTests.Lab1;

public class PaymentsTests
{
    [TestCase(PaymentsPlan.Annuity, 7, 3, 10000, 10116.9)]
    [TestCase(PaymentsPlan.Differentiated, 3, 5, 200000, 201500)]
    [TestCase(PaymentsPlan.Annuity, 10, 12, 100000, 105499.1)]
    [TestCase(PaymentsPlan.Annuity, 15, 6, 50000, 52210.1)]
    [TestCase(PaymentsPlan.Differentiated, 10, 12, 100000, 105416.7)]
    public void TestPasses_When_Result_Correct(PaymentsPlan plan, decimal rate, int monthsCount, decimal amount, decimal expected)
    {
        var actual = Payments.CalculateTotalPayments(plan, rate, monthsCount, amount);
        Assert.That(actual, Is.EqualTo(expected));
    }
}