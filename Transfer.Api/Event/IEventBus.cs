namespace Transfer.Api.Event;

public interface IEventBus
{
    void Publish<T>(T message, string queue);
}
