using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    private static User CreateUser() =>
        new("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", "+79131234567");

    [Test]
    public void GetUserFullName_Returns_NameAndSurname()
    {
        var user = CreateUser();
 
        Assert.That(user.GetUserFullName(), Is.EqualTo("Иван Петров"));
    }
    
    [TestCase("89131234567")]
    [TestCase("+79131234567")]
    [TestCase("+70000000000")]
    [TestCase("+7 913 123-45-67")]
    [TestCase("+7 913 123 45 67")]
    [TestCase("8-000-000-00-00")]
    public void TryUpdatePhone_Returns_True_When_PhoneValid(string phone)
    {
        var user = CreateUser();
        
        Assert.That(user.TryUpdatePhone(phone), Is.True);
    }
    
    [TestCase("")]
    [TestCase("8913123456")]
    [TestCase("891312345678")]
    [TestCase("79131234567")]
    [TestCase("+19131234567")]
    [TestCase("+791a1234567")]
    [TestCase("+7(000)000-00-00")]
    [TestCase("+7 000 000 00 0")]
    [TestCase("+7 - - - ")]
    public void TryUpdatePhone_Returns_False_When_PhoneNotValid(string phone)
    {
        var user = CreateUser();
        
        Assert.That(user.TryUpdatePhone(phone), Is.False);
    }

    [TestCase("")]
    [TestCase("8913123456")]
    [TestCase("891312345678")]
    [TestCase("79131234567")]
    [TestCase("+19131234567")]
    [TestCase("+791a1234567")]
    [TestCase("+7(000)000-00-00")]
    [TestCase("+7 000 000 00 0")]
    [TestCase("+7 - - - ")]
    public void Constructor_Throws_Exception_When_PhoneNotValid(string phone)
    {
        var isThrown = false;

        try
        {
            new User("ivan_petrov", "qwerty123", "Иван", "Петров", "540123456789", phone);
        }
        catch (ArgumentException)
        {
            isThrown = true;
        }
        
        Assert.That(isThrown, Is.True);
    }
    
    [Test]
    public void TryUpdatePhone_Returns_ExpectedResults_When_CalledSeveralTimes()
    {
        var user = CreateUser();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(user.TryUpdatePhone("89990001122"), Is.True);
            Assert.That(user.TryUpdatePhone("abc"), Is.False);
            Assert.That(user.TryUpdatePhone("+79990001122"), Is.True);
        }
    }
}