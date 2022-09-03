using RabbitMQ.Client;

namespace KebabMenuApplication.Options;

public class RabbitMqConnection
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public ConnectionFactory CreateFactory()
    {
        return new () { HostName = HostName, UserName = UserName, Password = Password };
    }
}