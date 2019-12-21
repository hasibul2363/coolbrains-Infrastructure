namespace CoolBrains.Infrastructure.Bus
{
    public abstract class BusQueueMessage : BusMessage, IBusQueueMessage
    {
        public string QueueName { get; set; }
    }
}
