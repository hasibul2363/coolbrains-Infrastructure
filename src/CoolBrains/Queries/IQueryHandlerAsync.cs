using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Queries
{
    public interface IQueryHandlerAsync<in TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
