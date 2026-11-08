using App.Lab3;

namespace AppTests.Lab3;

public class UserCreatorTests
{
    private static User CreateUser(string password)
    {
        return UserCreator.CreateUser("ivan_petrov", password, "Иван", "Петров", "540123456789", "+79131234567");
    }

    [TestCase("", "d41d8cd98f00b204e9800998ecf8427e")]
    [TestCase("frog", "938c2cc0dcc05f2b68c4287040cfcf71")]
    [TestCase("This is a message sent by a computer user.", "922547e866c89b8f677312df0ccec8ee")]
    public void CreateUser_CreatesValidHash(string password, string expectedHash)
    {
        var user = CreateUser(password);
        
        Assert.That(user.PasswordHash, Is.EqualTo(expectedHash));
    }

    [Test]
    public void CreateUser_DoesNotStore_PlainPassword()
    {
        const string password = "test";
        var user = CreateUser(password);
        
        Assert.That(user.PasswordHash, Is.Not.EqualTo(password));
    }

    [Test]
    public void SamePassword_Give_SameHashes()
    {
        const string password = "test";
        Assert.That(CreateUser(password).PasswordHash, Is.EqualTo(CreateUser(password).PasswordHash));
    }
    
    [Test]
    public void DifferentPassword_Give_DifferentHashes()
    {
        Assert.That(CreateUser("123").PasswordHash, Is.Not.EqualTo(CreateUser("124").PasswordHash));
    }
    
    [Test]
    public void CreateUser_Passes_OtherFields()
    {
        var user = UserCreator.CreateUser("masha", "pwd", "Мария", "Сидорова", "770987654321", "89001112233");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(user.Login, Is.EqualTo("masha"));
            Assert.That(user.GetUserFullName(), Is.EqualTo("Мария Сидорова"));
            Assert.That(user.Inn, Is.EqualTo("770987654321"));
            Assert.That(user.Phone, Is.EqualTo("89001112233"));
            Assert.That(user.Id, Is.Not.EqualTo(Guid.Empty));
        }
    }

    [Test]
    public void CreateUser_Throws_When_InvalidPhone()
    {
        Assert.Throws<ArgumentException>(() =>
            UserCreator.CreateUser("masha", "pwd", "Мария", "Сидорова", "770987654321", "8900111223"));
    }
}