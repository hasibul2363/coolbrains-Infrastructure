namespace CoolBrains.Infrastructure.Bus
{
    public abstract class BusQueueMessage : Message, IBusQueueMessage
    {
        public string QueueName { get; set; }

        public void SetQueueName()
        {
            if (string.IsNullOrEmpty(QueueName))
            {
                QueueName = this.GetType().Namespace;
            }
        }
    }
}
