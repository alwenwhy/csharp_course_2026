namespace App.Lab3;

public class User
{
    private const int PhoneDigitsAfterCountryCode = 10;

    public Guid Id { get; init; }
    public string Login { get; init; }
    public DateTime RegisterDate { get; init; }

    public string Inn { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    private string phone;
    
    public string Phone
    {
        get => phone;
        set
        {
            if (!TryUpdatePhone(value))
                throw new ArgumentException("Invalid phone number");
        }
    }

    public User()
    {
        Id = Guid.NewGuid();
        RegisterDate = DateTime.Now;
    }

    public User(string login, string passwordHash, string name, string surname, string inn, string phone) : this()
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
        Surname = surname;
        Inn = inn;
        Phone = phone;
    }
    
    public string GetUserFullName() => $"{Name} {Surname}";

    //на всякий случай хеш пихать сюда не буду
    public string GetUserInfo() =>
        $"{GetUserFullName()} ({Login}), тел. {Phone}, ИНН {Inn}, id {Id}, " +
        $"зарегистрирован {RegisterDate:yyyy-MM-dd HH:mm} UTC";
    
    private bool TryUpdatePhone(string newPhone)
    {
        if (!IsPhoneValid(newPhone))
            return false;
        
        this.phone = newPhone;
        return true;
    }
    
    private static bool IsPhoneValid(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return false;

        int start;
        if (phone.StartsWith("+7"))
            start = 2;
        else if (phone.StartsWith("8"))
            start = 1;
        else
            return false;

        var digitsCount = 0;
        for (var i = start; i < phone.Length; i++)
        {
            if (phone[i] == ' ' || phone[i] == '-')
                continue;
 
            if (!char.IsAsciiDigit(phone[i]))
                return false;
 
            digitsCount++;
        }
        
        return digitsCount == PhoneDigitsAfterCountryCode;
    }
}