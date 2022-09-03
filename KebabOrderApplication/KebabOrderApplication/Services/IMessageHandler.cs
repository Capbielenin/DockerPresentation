namespace KebabOrderApplication.Services;

public interface IMessageHandler
{
    Task HandleMessage(string message);
}