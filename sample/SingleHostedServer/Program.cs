using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus.RabbitMQ;
using CoolBrains.Infrastructure;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Events;
using CoolBrains.Infrastructure.Extensions;
using CoolBrains.Infrastructure.Queries;
using CoolBrains.Infrastructure.Session;
using CoolBrains.Infrastructure.Store.Mongo;
using CoolBrains.Infrastructure.Store.Mongo.Extensions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SingleHostedServer.Command;
using SingleHostedServer.Event;
using SingleHostedServer.Event.User;
using SingleHostedServer.Query;

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
            services.AddMassTransit();

            services.AddSingleton<UserContext>(new UserContext
            {
                UserId = Guid.NewGuid(), TenantId = Guid.Parse("97f1c1d9-4219-4fc3-adf4-19fdb9dd8846")
            });

            services.AddTransient<ICommandHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddTransient<ICommandHandler<UserUpdateCommand>, UserUpdateCommandHandler>();
            services.AddScoped<IQueryHandler<UserQuery, List<UserInfo>>, UserQueryHandler>();
            //services.AddTransient<IEventHandlerAsync<UserCreated>, UserCreatedEventHandler>();
            services.AddTransient<UserCreatedEventHandler>();




            var builder = services
                    .AddCoolBrains()
                .AddMongoDbProvider(_configuration);
            _serviceProvider = services.BuildServiceProvider();

            builder.AddRabbitMqProvider(_configuration)
                .Listen(
                    e =>
                    {
                        e.Consumer<UserCreatedEventHandler>(_serviceProvider);
                        //e.ConfigureConsumer<UserCreatedEventHandler>(_serviceProvider);
                    }
                    ).Start();

            services.Configure<DbConnectionDetails>(_configuration.GetSection("DbConnectionDetails"));
            _serviceProvider = services.BuildServiceProvider();

            
        }

        static async Task Main(string[] args)
        {
            BuildConfiguration();
            Register();
            //var asm = Assembly.Load("SingleHostedServer");

            var bus = _serviceProvider.GetService<IDispatcher>();
            while (true)
            {
                var command = new CreateUserCommand
                {
                    Email = "hasibul2363@gmail.com",
                    UserName = "hasibul2363",
                    UserId = Guid.Parse("f5414aad-69b8-4a81-bebf-45d9dbbd71df")
                };

                //var response = bus.Send(command);

                var query = new UserQuery{ SearchText = "ha"};
                var users = bus.GetResult(query);
                Console.WriteLine($"Query: Total user found {users.Count}");



                var updateResponse = bus.Send(new UserUpdateCommand {Id = Guid.Parse("f5414aad-69b8-4a81-bebf-45d9dbbd71df"), UserName = "masud5"});


                Console.WriteLine("q to exit");
                var s = Console.ReadLine();
                if (s == "q")
                {
                    RabbitMqBusMessageListener.Stop();
                    break;
                }
            }
            
            
            Console.WriteLine("Hello World!");
        }

    }
}
