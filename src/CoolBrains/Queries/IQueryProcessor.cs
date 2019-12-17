using System.Threading.Tasks;

namespace CoolBrains.Infrastructure.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
        TResult Process<TResult>(IQuery<TResult> query);
    }
}
