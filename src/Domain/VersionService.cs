using System;
using CoolBrains.Infrastructure.Exceptions;

namespace CoolBrains.Infrastructure.Domain
{
    public class VersionService : IVersionService
    {
        public int GetNextVersion(Guid aggregateRootId, int currentVersion, int? expectedVersion)
        {
            if (expectedVersion.HasValue && expectedVersion.Value > 0 && expectedVersion.Value != currentVersion)
            {
                throw new ConcurrencyException(aggregateRootId, expectedVersion.Value, currentVersion);
            }

            return currentVersion + 1;
        }
    }
}