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

    [Test]
    public void GetUserInfo_Returns_UserData()
    {
        var user = CreateUser();
        var info = user.GetUserInfo();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(info, Does.Contain("ivan_petrov"));
            Assert.That(info, Does.Contain("Иван Петров"));
            Assert.That(info, Does.Contain("540123456789"));
            Assert.That(info, Does.Contain("+79131234567"));
            Assert.That(info, Does.Contain(user.Id.ToString()));
        }
    }
    
    [TestCase("89131234567")]
    [TestCase("+79131234567")]
    [TestCase("+70000000000")]
    [TestCase("+7 913 123-45-67")]
    [TestCase("+7 913 123 45 67")]
    [TestCase("8-000-000-00-00")]
    public void Phone_Returns_NewPhone_When_PhoneValid(string phone)
    {
        var user = CreateUser();
        user.Phone = phone;
        
        Assert.That(user.Phone, Is.EqualTo(phone));
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
    public void Phone_Throws_Exception_When_PhoneNotValid(string phone)
    {
        var user = CreateUser();
        
        Assert.Throws<ArgumentException>(() => user.Phone = phone);
        Assert.That(user.Phone, Is.EqualTo("+79131234567"));
    }
    
    [Test]
    public void TryUpdatePhone_IsNotPublic()
    {
        Assert.That(typeof(User).GetMethod("TryUpdatePhone"), Is.Null);
    }
}