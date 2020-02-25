using System;
using System.Collections.Generic;

namespace CoolBrains.Infrastructure.CommonTypes
{
    public class RoleBasedSecurity
    {
        public RoleBasedSecurity()
        {
            IdsAllowedToCreate = new List<Guid>();
            IdsAllowedToRead = new List<Guid>();
            IdsAllowedToUpdate = new List<Guid>();
            IdsAllowedToDelete = new List<Guid>();
            IdsAllowedToShare = new List<Guid>();

            RolesAllowedToCreate  = new List<Guid>();
            RolesAllowedToRead = new List<string>();
            RolesAllowedToDelete = new List<string>();
            RolesAllowedToUpdate = new List<string>();
            RolesAllowedToShare = new List<string>();

        }

        public List<Guid> IdsAllowedToCreate { get; set; }
        public List<Guid> IdsAllowedToRead { get; set; }
        public List<Guid> IdsAllowedToUpdate { get; set; }
        public List<Guid> IdsAllowedToDelete { get; set; }
        public List<Guid> IdsAllowedToShare { get; set; }


        public List<Guid> RolesAllowedToCreate { get; set; }
        public List<string> RolesAllowedToDelete { get; set; }
        public List<string> RolesAllowedToUpdate { get; set; }
        public List<string> RolesAllowedToRead { get; set; }
        public List<string> RolesAllowedToShare { get; set; }


    }
}
