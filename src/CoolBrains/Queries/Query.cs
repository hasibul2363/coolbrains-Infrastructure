using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Queries
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool DoCount { get; set; } = true;
        public UserContext UserContext { get; private set; }
        public void SetUserContext(UserContext userContext)
        {
            UserContext = userContext;
        }
    }
}
