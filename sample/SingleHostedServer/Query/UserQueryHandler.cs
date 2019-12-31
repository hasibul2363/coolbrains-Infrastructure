using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolBrains.Infrastructure.Queries;
using CoolBrains.Infrastructure.Store.Abstraction;
using SingleHostedServer.Event.User;

namespace SingleHostedServer.Query
{
    public class UserQueryHandler : IQueryHandler<UserQuery, List<UserInfo>>
    {
        private readonly IRepository _repository;

        public UserQueryHandler(IRepository repository)
        {
            _repository = repository;
        }
        public List<UserInfo> Handle(UserQuery query)
        {
            var users = _repository.GetItems<UserInfo>(p => p.UserName.Contains(query.SearchText)).ToList();
            return users;
        }
    }
}
