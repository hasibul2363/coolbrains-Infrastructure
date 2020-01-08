using System;
using System.Collections.Generic;
using System.Text;
using CoolBrains.Infrastructure.Commands;
using CoolBrains.Infrastructure.Domain;
using SingleHostedServer.Domain;

namespace SingleHostedServer.Command
{
    public class UserUpdateCommandHandler : ICommandHandler<UserUpdateCommand>
    {
        private readonly IDomainRepository<User> _domainRepository;
        public UserUpdateCommandHandler(IDomainRepository<User> domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public CommandResponseWithEvents Handle(UserUpdateCommand command)
        {
            var commandResponse = new CommandResponseWithEvents();
            var user = _domainRepository.GetById(command.Id);
            user.Update(command.UserName);
            commandResponse.Events = user.Events;
            
            return commandResponse;
        }
    }
}
