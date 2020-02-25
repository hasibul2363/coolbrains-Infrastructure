using System;
using System.Collections.Generic;
using System.Linq;

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

            RolesAllowedToCreate = new List<string>();
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


        public List<string> RolesAllowedToCreate { get; set; }
        public List<string> RolesAllowedToDelete { get; set; }
        public List<string> RolesAllowedToUpdate { get; set; }
        public List<string> RolesAllowedToRead { get; set; }
        public List<string> RolesAllowedToShare { get; set; }


    }

    public static class RoleBasedSecurityExtension
    {
        public static void ApplyPermission(this RoleBasedSecurity roleBasedSecurity, List<Guid> ids, List<ActionItemEnum> actionItems)
        {
            if (ids == null)
                return;
            actionItems.ForEach(action =>
            {
                switch (action)
                {
                    case ActionItemEnum.Create:
                        var changedItems = ids.Except(roleBasedSecurity.IdsAllowedToCreate).ToList();
                        if (changedItems.Any())
                            roleBasedSecurity.IdsAllowedToCreate.AddRange(changedItems);
                        break;
                    case ActionItemEnum.Read:
                        changedItems = ids.Except(roleBasedSecurity.IdsAllowedToRead).ToList();
                        if (changedItems.Any())
                            roleBasedSecurity.IdsAllowedToRead.AddRange(changedItems);
                        break;
                    case ActionItemEnum.Update:
                        changedItems = ids.Except(roleBasedSecurity.IdsAllowedToUpdate).ToList();
                        if (changedItems.Any())
                            roleBasedSecurity.IdsAllowedToUpdate.AddRange(changedItems);
                        break;
                    case ActionItemEnum.Delete:
                        changedItems = ids.Except(roleBasedSecurity.IdsAllowedToDelete).ToList();
                        if (changedItems.Any())
                            roleBasedSecurity.IdsAllowedToDelete.AddRange(changedItems);
                        break;
                    case ActionItemEnum.Share:
                        changedItems = ids.Except(roleBasedSecurity.IdsAllowedToShare).ToList();
                        if (changedItems.Any())
                            roleBasedSecurity.IdsAllowedToShare.AddRange(changedItems);
                        break;
                }
            });
        }
        public static void ApplyPermission(this RoleBasedSecurity roleBasedSecurity, List<string> roles, List<ActionItemEnum> actionItems)
        {
            if (roles == null)
                return;
            actionItems.ForEach(action =>
            {
                switch (action)
                {
                    case ActionItemEnum.Create:
                        var changedRoles = roles.Except(roleBasedSecurity.RolesAllowedToCreate).ToList();
                        if (changedRoles.Any())
                            roleBasedSecurity.RolesAllowedToCreate.AddRange(changedRoles);
                        break;
                    case ActionItemEnum.Read:
                        changedRoles = roles.Except(roleBasedSecurity.RolesAllowedToRead).ToList();
                        if (changedRoles.Any())
                            roleBasedSecurity.RolesAllowedToRead.AddRange(changedRoles);
                        break;
                    case ActionItemEnum.Update:
                        changedRoles = roles.Except(roleBasedSecurity.RolesAllowedToUpdate).ToList();
                        if (changedRoles.Any())
                            roleBasedSecurity.RolesAllowedToUpdate.AddRange(changedRoles);
                        break;
                    case ActionItemEnum.Delete:
                        changedRoles = roles.Except(roleBasedSecurity.RolesAllowedToDelete).ToList();
                        if (changedRoles.Any())
                            roleBasedSecurity.RolesAllowedToDelete.AddRange(changedRoles);
                        break;
                    case ActionItemEnum.Share:
                        changedRoles = roles.Except(roleBasedSecurity.RolesAllowedToShare).ToList();
                        if (changedRoles.Any())
                            roleBasedSecurity.RolesAllowedToShare.AddRange(changedRoles);
                        break;
                }
            });
        }
        public static void RevokePermission(this RoleBasedSecurity roleBasedSecurity, List<Guid> ids, List<ActionItemEnum> actionItems)
        {
            actionItems.ForEach(action =>
            {
                if (ids == null)
                    return;
                switch (action)
                {
                    case ActionItemEnum.Create:
                        roleBasedSecurity.IdsAllowedToCreate.RemoveAll(ids.Contains);
                        break;
                    case ActionItemEnum.Read:
                        roleBasedSecurity.IdsAllowedToRead.RemoveAll(ids.Contains);
                        break;
                    case ActionItemEnum.Update:
                        roleBasedSecurity.IdsAllowedToUpdate.RemoveAll(ids.Contains);
                        break;
                    case ActionItemEnum.Delete:
                        roleBasedSecurity.IdsAllowedToDelete.RemoveAll(ids.Contains);
                        break;
                    case ActionItemEnum.Share:
                        roleBasedSecurity.IdsAllowedToShare.RemoveAll(ids.Contains);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, null);
                }
            });
        }
        public static void RevokePermission(this RoleBasedSecurity roleBasedSecurity, List<string> roles, List<ActionItemEnum> actionItems)
        {
            if (roles == null) return;

            actionItems.ForEach(action =>
            {
                switch (action)
                {
                    case ActionItemEnum.Create:

                        roleBasedSecurity.RolesAllowedToCreate.RemoveAll(roles.Contains);
                        break;
                    case ActionItemEnum.Read:
                        roleBasedSecurity.RolesAllowedToRead.RemoveAll(roles.Contains);
                        break;
                    case ActionItemEnum.Update:
                        roleBasedSecurity.RolesAllowedToUpdate.RemoveAll(roles.Contains);
                        break;
                    case ActionItemEnum.Delete:
                        roleBasedSecurity.RolesAllowedToDelete.RemoveAll(roles.Contains);
                        break;
                    case ActionItemEnum.Share:
                        roleBasedSecurity.RolesAllowedToShare.RemoveAll(roles.Contains);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, null);
                }
            });
        }
    }
}
