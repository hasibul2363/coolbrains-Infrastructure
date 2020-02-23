using CoolBrains.Infrastructure.Session;

namespace CoolBrains.Infrastructure.Queries
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        protected Query()
        {
            PageNumber = 1;
            PageSize = 10;
            DoCount = false;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool DoCount { get; set; } = true;
        public UserContext UserContext { get; private set; }
        public void SetUserContext(UserContext userContext)
        {
            UserContext = userContext;
        }

        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
    }
}
