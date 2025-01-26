namespace webCollege.Models;

public class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int Age { get; set; }

    public string FormatMessage()
    {
        return $"Новая заявка! \n Имя клиента: {Name} \n Возраст: {Age} \n Почта: {Email} \n Телефон: {Phone}";
    }
}