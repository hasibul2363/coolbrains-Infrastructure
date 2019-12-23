namespace CoolBrains.Infrastructure.Bus.RabbitMQ
{
    public class RabbitConfig
    {
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }

    }
}
