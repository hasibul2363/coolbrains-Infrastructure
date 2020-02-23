namespace CoolBrains.Infrastructure.Bus
{
    public interface IBusTopicMessage : IMessage
    {
        string TopicName { get; set; }
    }
}
