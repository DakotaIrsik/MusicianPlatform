using System;
using System.ComponentModel;
using System.Linq;

namespace CelestialMediaGroup.Permissions.Extensions
{
    public static class PermissionChecker
    {
        public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();

            if (!Enum.TryParse(permissionName, true, out ApplicationPermissions permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(ApplicationPermissions)}.");

            return usersPermissions.Contains(permissionToCheck);
        }
    }
}
