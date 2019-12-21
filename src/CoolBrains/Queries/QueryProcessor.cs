using System;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Dependencies;
using CoolBrains.Infrastructure.Exceptions;
using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Queries
{
    
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IHandlerResolver _handlerResolver;
        private UserContext _userContext;
        public QueryProcessor(IHandlerResolver handlerResolver, UserContext userContext)
        {
            _handlerResolver = handlerResolver;
            _userContext = userContext;
        }

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            _userContext = query.UserContext;
            var handler = _handlerResolver.ResolveQueryHandler(query, typeof(IQueryHandlerAsync<,>));
            var handleMethod = handler.GetType().GetMethod("HandleAsync", new[] { query.GetType() });
            return (Task<TResult>)handleMethod.Invoke(handler, new object[] { query });
        }

        
        public TResult Process<TResult>(IQuery<TResult> query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            _userContext = query.UserContext;
            var handler = _handlerResolver.ResolveQueryHandler(query, typeof(IQueryHandler<,>));
            var handleMethod = handler.GetType().GetMethod("Handle", new[] { query.GetType() });
            return (TResult)handleMethod.Invoke(handler, new object[] { query });
        }
    }
}