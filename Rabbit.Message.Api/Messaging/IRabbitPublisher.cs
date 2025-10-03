namespace Rabbit.Message.Api.Messaging
{
    public interface IRabbitPublisher
    {
        Task PublishAsync<T>(string queueName, T message);
    }
}
