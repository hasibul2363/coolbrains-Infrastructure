﻿namespace CoolBrains.Infrastructure.Bus
{
    public interface IBusQueueMessage : IBusMessage
    {
        string QueueName { get; set; }
        void SetQueueName();
    }
}
