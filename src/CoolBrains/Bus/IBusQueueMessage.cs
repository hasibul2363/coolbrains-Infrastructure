namespace CoolBrains.Infrastructure.Bus
{
    public interface IBusQueueMessage : IMessage
    {
        string QueueName { get; set; }
        void SetQueueName();
    }
}
