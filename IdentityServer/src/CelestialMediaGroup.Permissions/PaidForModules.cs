using System;

namespace CelestialMediaGroup.Permissions
{
    [Flags]
    public enum PaidForModules : long
    {
        None = 0,
        Feature1 = 1,
        Feature2 = 2,
        Feature3 = 4
    }
}
