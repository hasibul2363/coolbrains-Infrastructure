using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.OAuth
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
