﻿using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using CoolBrains.Infrastructure;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Extensions;
using CoolBrains.Infrastructure.Session;
using CoolBrains.Infrastructure.Store.Mongo;
using CoolBrains.Infrastructure.Store.Mongo.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SingleHostedServer.Command;
using SingleHostedServer.Event;

namespace SingleHostedServer
{
    class Program
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;
        private static void BuildConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
        }

        private static void Register()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<UserContext>(new UserContext
            {
                UserId = Guid.NewGuid(), TenantId = Guid.Parse("97f1c1d9-4219-4fc3-adf4-19fdb9dd8846")
            });

            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<IEventHandlerAsync<UserCreated>, UserCreatedEventHandler>();
            
            services.AddCoolBrains().AddMongoDbProvider(_configuration);
            //services.Configure<DbConnectionDetails>(_configuration.GetSection("DbConnectionDetails"));
            _serviceProvider = services.BuildServiceProvider();

            
        }

        static async Task Main(string[] args)
        {
            BuildConfiguration();
            Register();
            var command = new CreateUserCommand
            {
                Email = "hasibul2363@gmail.com", UserName = "hasibul2363"
            };

            var bus = _serviceProvider.GetService<IDispatcher>();
            var response = bus.Send(command);
            Console.WriteLine("Hello World!");
        }
    }
}
