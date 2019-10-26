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
            builder.AppendLine(GetTopColor(time.Seconds));
            builder.AppendLine(GetHours(time.Hours));
            builder.Append(GetMinutes(time.Minutes));

            return builder.ToString();
        }

        private string GetTopColor(int seconds)
        {
            return seconds % 2 == 0 ? Colors.Yellow : Colors.Off;
        }

        private string GetHours(int hours)
        { 
            var top = GetLampStates(hours / 5, 4, Colors.Red);
            var bottom = GetLampStates(hours % 5, 4, Colors.Red);

            top.Add(Environment.NewLine);
            top.AddRange(bottom);

            return string.Join("", top);
        }

        private string GetMinutes(int minutes)
        {
            var top = GetLampStates(minutes / 5, 11, Colors.Yellow);
            var bottom = GetLampStates(minutes % 5, 4, Colors.Yellow);

            top.Add(Environment.NewLine);
            var minutesTop = string.Join("", top).Replace("YYY", "YYR");

            return minutesTop + string.Join("", bottom);
        }

        private List<string> GetLampStates(int activeLamps, int numberOfLamps, string color)
        {
            var lamps = new List<string>();
            for(int i = 1; i <= numberOfLamps; i++)
            {
                lamps.Add((i <= activeLamps ? color : Colors.Off));
            }
            return lamps;
        }   
        
         
    }
}
