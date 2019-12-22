﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Bus.RabbitMQ
{
    public class RabbitConfig
    {
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }

    }
}
