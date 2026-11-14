namespace App.Lab3;

public class User
{
    private const int PhoneDigitsAfterCountryCode = 10;
    
    private readonly Guid id;
    private readonly string login;
    private readonly DateTime registerDate;

    private string inn;
    private string password;
    private string name;
    private string surname;
    private string phone;

    public User(string login, string password, string name, string surname, string inn, string phone)
    {
        if (!TryUpdatePhone(phone))
            throw new ArgumentException("Phone is not valid");
        
        id = Guid.NewGuid();
        registerDate = DateTime.Now;
        
        this.login = login;
        this.password = password;
        this.name = name;
        this.surname = surname;
        this.inn = inn;
    }
    
    public string GetUserFullName() => $"{name} {surname}";
    
    public bool TryUpdatePhone(string phone)
    {
        if (!IsPhoneValid(phone))
            return false;
        
        this.phone = phone;
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