using App.Lab3;

namespace AppTests.Lab3;

public class UserTests
{
    private const string ValidPhone = "+79131234567";
    
    private static User CreateUser(string name = "Иван", string surname = "Петров", string phone = ValidPhone)
    {
        return new User("ivan_petrov", "qwerty123", name, surname, "540123456789", phone);
    }

    [TestCase("Иван", "Петров", "Иван Петров")]
    [TestCase("Мария", "Сидорова", "Мария Сидорова")]
    [TestCase("Родион", "Раскольников", "Родион Раскольников")]
    public void GetUserFullName_Returns_NameAndSurname(string name, string surname, string expected)
    {
        var user = CreateUser(name, surname);
 
        Assert.That(user.GetUserFullName(), Is.EqualTo(expected));
    }

    [TestCase(ValidPhone)]
    [TestCase("89131234567")]
    [TestCase("+70000000000")]
    public void GetUserPhone_Returns_True_When_PhoneValid(string phone)
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
    [TestCase("+7 000 000 00 00")]
    public void GetUserPhone_Returns_False_When_PhoneNotValid(string phone)
    {
        var user = CreateUser();
        
        Assert.That(user.TryUpdatePhone(phone), Is.False);
    }

    [TestCase("")]
    [TestCase("8913123456")]
    [TestCase("891312345678")]
    public void Constructor_Throws_Exception_When_PhoneNotValid(string phone)
    {
        Assert.Throws<ArgumentException>(() => CreateUser(phone: phone));
    }
    
    [Test]
    public void TryUpdatePhone_CanBeCalledSeveralTimes()
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