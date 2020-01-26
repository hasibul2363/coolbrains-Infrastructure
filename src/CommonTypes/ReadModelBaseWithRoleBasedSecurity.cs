using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.CommonTypes
{
    public abstract class ReadModelBaseWithRoleBasedSecurity : RoleBasedSecurity
    {
        protected ReadModelBaseWithRoleBasedSecurity()
        {
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }

        public void SetDefaultForCreation(Guid userId)
        {
            CreatedBy = userId;
            UpdatedBy = userId;
        }

        public void SetDefaultForUpdate(Guid userId)
        {
            UpdatedBy = userId;
            DateUpdated = DateTime.UtcNow;
        }
    }
}
