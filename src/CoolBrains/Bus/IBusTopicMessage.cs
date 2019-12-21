namespace CoolBrains.Infrastructure.Bus
{
    public interface IBusTopicMessage : IBusMessage
    {
        string TopicName { get; set; }
    }
}
