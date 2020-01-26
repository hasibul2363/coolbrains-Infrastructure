using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBrains.Infrastructure.Security
{
    public class RoleBasedSecurity
    {
        public RoleBasedSecurity()
        {
            IdsAllowedToRead = new List<Guid>();
            IdsAllowedToUpdate = new List<Guid>();
            IdsAllowedToDelete = new List<Guid>();

            RolesAllowedToRead = new List<string>();
            RolesAllowedToDelete = new List<string>();
            RolesAllowedToUpdate = new List<string>();

        }
        public List<Guid> IdsAllowedToRead { get; set; }
        public List<Guid> IdsAllowedToUpdate { get; set; }
        public List<Guid> IdsAllowedToDelete { get; set; }
        public List<string> RolesAllowedToDelete { get; set; }
        public List<string> RolesAllowedToUpdate { get; set; }
        public List<string> RolesAllowedToRead { get; set; }


    }
}
