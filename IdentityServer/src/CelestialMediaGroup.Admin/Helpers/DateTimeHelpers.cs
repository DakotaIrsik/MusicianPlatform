﻿using System;

namespace CelestialMediaGroup.Admin.Helpers
{
    public static class DateTimeHelpers
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetEpochTicks(this DateTimeOffset dateTime)
        {
            return dateTime.Subtract(Epoch).TotalMilliseconds;
        }
    }
}