using BerlinClock.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BerlinClock.Classes
{
    public interface IClockFormat
    {
        string GetTime(string aTime);
    }

    public class BerlinClockFormat : IClockFormat
    {
        private readonly ITimeFormatter timeFormatter;

        public BerlinClockFormat(ITimeFormatter timeFormatter)
        {
            this.timeFormatter = timeFormatter;
        }

        public string GetTime(string aTime)
        {
            var time = timeFormatter.GetTime(aTime);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetLampColor(time.Seconds));
            builder.AppendLine(GetHours(time.Hours));
            //builder.AppendLine(GetMinutesLevel1(time.Minutes));
            builder.Append(GetMinutes(time.Minutes));

            return builder.ToString();
        }

        private string GetLampColor(int seconds)
        {
            return seconds % 2 == 0 ? Colors.Yellow : Colors.Off;
        }

        private string GetHours(int hours)
        { 
            var level1 = GetLampState(() => { return hours / 5; }, 4, Colors.Red);
            var level2 = GetLampState(() => { return hours % 5; }, 4, Colors.Red);

            level1.Add(Environment.NewLine);
            level1.AddRange(level2);

            return string.Join("", level1);
        }

        private List<string> GetLampState(Func<int> func, int lamps, string color)
        {
            int activeLamps = func();
            var list = new List<string>();

            for(int i = 1; i <= lamps; i++)
            {
                list.Add((i <= activeLamps ? color : Colors.Off));
            }           

            return list;
        }   
        
        private string GetMinutes(int minutes)
        {
            var level1 = GetMinutesLevel1(minutes);
            var level2 = GetLampState(() => { return minutes % 5; }, 4, Colors.Yellow);
            return level1 + Environment.NewLine + string.Join("", level2);
        }

        private string GetMinutesLevel1(int minutes)
        {
            int count = minutes / 5;
            var list = new List<string>();
            for (int i = 1; i <= 11; i++)
            {
                list.Add(GetLampState(count, i));
            }

            return string.Join("", list);
        }

        private static string GetLampState(int count, int i)
        {
            return (i <= count ? (i % 3 == 0 ? Colors.Red : Colors.Yellow) : Colors.Off);
        }
    }
}
