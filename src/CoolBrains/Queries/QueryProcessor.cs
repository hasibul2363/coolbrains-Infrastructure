using System;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Exceptions;

namespace CoolBrains.Infrastructure.Queries
{
    
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IHandlerResolver _handlerResolver;

        public QueryProcessor(IHandlerResolver handlerResolver)
        {
            _handlerResolver = handlerResolver;
        }

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = _handlerResolver.ResolveQueryHandler(query, typeof(IQueryHandlerAsync<,>));
            var handleMethod = handler.GetType().GetMethod("HandleAsync", new[] { query.GetType() });
            return (Task<TResult>)handleMethod.Invoke(handler, new object[] { query });
        }

        
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var handler = _handlerResolver.ResolveQueryHandler(query, typeof(IQueryHandler<,>));
            var handleMethod = handler.GetType().GetMethod("Handle", new[] { query.GetType() });
            return (TResult)handleMethod.Invoke(handler, new object[] { query });
        }
    }
}