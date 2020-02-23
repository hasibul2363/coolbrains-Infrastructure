using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Bus.RabbitMQ;
using CoolBrains.Infrastructure;
using CoolBrains.Infrastructure.Bus;
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

            services.AddScoped<UserContext>(p => new UserContext
            {
                UserId = Guid.NewGuid(),
                TenantId = Guid.Parse("97f1c1d9-4219-4fc3-adf4-19fdb9dd8846")
            });


            services.AddScoped<UserContext>(p => new UserContext());



            services.AddTransient<ICommandHandlerAsync<CreateUserCommand>, CreateUserCommandHandler>();
            //services.AddTransient(typeof(ICommandHandlerAsync<>), typeof(CommandHandlerAsync<>));
            services.AddScoped<ICommandHandlerAsync<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<CreateUserCommandHandler>();
            services.AddScoped<IQueryHandler<UserQuery, List<UserInfo>>, UserQueryHandler>();
            //services.AddTransient<IEventHandlerAsync<UserCreated>, UserCreatedEventHandler>();
            services.AddTransient<UserCreatedEventHandler>();




            var builder = services
                    .AddCoolBrains()
                .AddMongoDbProvider(_configuration);


            builder.AddRabbitMqProvider(_configuration)
                .Listen(
                    e =>
                    {
                        _serviceProvider = services.BuildServiceProvider();
                        //e.Consumer<CreateUserCommandHandler>(_serviceProvider);
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
                    Email = "hasibul236355@gmail.com",
                    UserName = "hasibul236355",
                    UserId = Guid.Parse("bff5b1ee-5fec-4096-9e0d-7201b30eea66")
                };

                var response = await bus.SendAsync(command);
                //await bus.SendBusMessageAsync(command);

                //var query = new UserQuery{ SearchText = "ha"};
                //var users = bus.GetResult(query);
                //Console.WriteLine($"Query: Total user found {users.Count}");



                //var updateResponse = bus.Send(new UserUpdateCommand {Id = Guid.Parse("bff5b1ee-5fec-4096-9e0d-7201b30eea66"), UserName = "masud5"});


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
