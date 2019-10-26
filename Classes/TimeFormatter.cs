using System;
using System.Linq;

namespace BerlinClock.Classes
{
    public interface ITimeFormatter
    {
        Time GetTime(string aTime);
    }

    public class TimeFormatter : ITimeFormatter
    {
        public Time GetTime(string aTime)
        {
            var time = aTime.Split(':');
            if (time?.Any() != true)
            {
                throw new ApplicationException(string.Format("Time format is not valid!"));
            }

            return new Time(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
        }
    }
}
