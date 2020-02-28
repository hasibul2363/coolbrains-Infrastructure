using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ActionItemEnum> GetActionItems(Guid? userId, List<string> roles = null)
        {
            var actionItems = new List<ActionItemEnum>();
            if (userId.HasValue)
            {
                if (IdsAllowedToCreate.Contains(userId.Value))
                    actionItems.Add(ActionItemEnum.Create);
                if (IdsAllowedToRead.Contains(userId.Value))
                    actionItems.Add(ActionItemEnum.Read);
                if (IdsAllowedToUpdate.Contains(userId.Value))
                    actionItems.Add(ActionItemEnum.Update);
                if (IdsAllowedToDelete.Contains(userId.Value))
                    actionItems.Add(ActionItemEnum.Delete);
                if (IdsAllowedToShare.Contains(userId.Value))
                    actionItems.Add(ActionItemEnum.Share);
            }

            if (roles!=null)
            {
                if (RolesAllowedToCreate.Intersect(roles).Any())
                    actionItems.Add(ActionItemEnum.Create);
                if (RolesAllowedToRead.Intersect(roles).Any())
                    actionItems.Add(ActionItemEnum.Read);
                if (RolesAllowedToUpdate.Intersect(roles).Any())
                    actionItems.Add(ActionItemEnum.Update);
                if (RolesAllowedToDelete.Intersect(roles).Any())
                    actionItems.Add(ActionItemEnum.Delete);
                if (RolesAllowedToShare.Intersect(roles).Any())
                    actionItems.Add(ActionItemEnum.Share);
            }

            return actionItems.Distinct().ToList();
        }
    }
}
