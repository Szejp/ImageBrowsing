using System;

namespace Script.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetHoursSinceCreated(DateTime timeCreated)
        {
            return (DateTime.Now - timeCreated).TotalHours.ToString("0");
        }
    }
}