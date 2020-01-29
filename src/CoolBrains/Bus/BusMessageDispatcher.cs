﻿using System;
using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Bus
{
    public class BusMessageDispatcher : IBusMessageDispatcher
    {
        private readonly IQueueClient _queueClient;
        private readonly ITopicClient _topicClient;

        public BusMessageDispatcher(IQueueClient queueClient, ITopicClient topicClient)
        {
            _queueClient = queueClient;
            _topicClient = topicClient;
        }

        public Task DispatchAsync<TMessage>(TMessage message) where TMessage : IMessage
        {
            if (message is IBusQueueMessage && message is IBusTopicMessage)
                throw new NotSupportedException("The message cannot implement both the IBusQueueMessage and the IBusTopicMessage interfaces");
            
            if (message is IBusQueueMessage queueMessage)
                return _queueClient.SendAsync(queueMessage);
            
            if (message is IBusTopicMessage topicMessage)
                return _topicClient.PublishAsync(topicMessage);
         
            throw new NotSupportedException("The message must implement either the IBusQueueMessage or the IBusTopicMessage interface");
        }

    }
}
