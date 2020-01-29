namespace CoolBrains.Infrastructure.Bus
{
    public abstract class BusTopicMessage : Message, IBusTopicMessage
    {
        public string TopicName { get; set; }
    }
}
