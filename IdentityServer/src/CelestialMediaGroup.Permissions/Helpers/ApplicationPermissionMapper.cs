using CelestialMediaGroup.Permissions.Extensions;
using System;
using System.Linq;

namespace CelestialMediaGroup.Permissions.Helpers
{
    public static class ApplicationPermissionMapper
    {
        public static string GetPermissionsForApp(string appName)
        {
            switch (appName)
            {
                case "Studios":
                    return Enum.GetValues(typeof(ApplicationPermissions)).Cast<ApplicationPermissions>().PackPermissionsIntoString();
                default:
                    return string.Empty;
            }
        }
    }
}
