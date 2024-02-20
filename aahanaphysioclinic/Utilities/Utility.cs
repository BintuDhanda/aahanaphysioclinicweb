namespace aahanaphysioclinic.Utilities
{
    public static class Utility
    {
        public static DateTime GetDateFromYearMonthDay(int Year, int Month, int Day)
        {
            return new DateTime(Year, Month, Day, 0, 0, 0, 0);
        }
        public static TimeSpan GetTimeFromHoursMinutes(int Hours, int Minutes, string Meridiem)
        {
            if (Meridiem == "PM" && Hours < 12)
            {
                Hours += 12;
            }
            else if (Meridiem == "AM" && Hours == 12)
            {
                Hours = 0;
            }

            return new TimeSpan(Hours, Minutes, 0);
        }

    }
}
