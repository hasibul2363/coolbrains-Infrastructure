﻿using System;
using CoolBrains.Infrastructure.Domain;
using CoolBrains.Infrastructure.Extensions;
using CoolBrains.Infrastructure.Store.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CoolBrains.Infrastructure.Store.Mongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static ICoolBrainsServiceBuilder AddCosmosDbMongoDbProvider(this ICoolBrainsServiceBuilder builder)
        //{
        //    return AddCosmosDbMongoDbProvider(builder, opt => { });
        //}

        public static ICoolBrainsServiceBuilder AddMongoDbProvider(this ICoolBrainsServiceBuilder builder, IConfiguration configuration)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            //if (setupAction == null)
            //{
            //    throw new ArgumentNullException(nameof(setupAction));
            //}

            //builder.Services.Configure(setupAction);

            builder.Services.Configure<DbConnectionDetails>(configuration.GetSection("DbConnectionDetails"));

            //builder.Services.AddScoped<IRepository, ClassicMongoRepository>();

            builder.Services.AddScoped<TenantSpecificDbConnectionBuilder>();
            builder.Services.AddScoped<IRepository, TenantSpecificMongoRepository>();
            builder.Services.AddScoped<IDomainStore, DomainStore>();


            return builder;
        }
    }
}
