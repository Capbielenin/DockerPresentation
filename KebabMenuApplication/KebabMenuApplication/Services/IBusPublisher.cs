namespace KebabMenuApplication.Services;

public interface IBusPublisher
{
    void PublishAsync(object message);
}