using BerlinClock.Common;
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
            builder.AppendLine(GetHoursTop(time.Hours));
            builder.AppendLine(GetHoursBottom(time.Hours));
            builder.AppendLine(GetMinutesTop(time.Minutes));
            builder.Append(GetMinutesBottom(time.Minutes));

            return builder.ToString();
        }

        private string GetTopColor(int seconds)
        {
            return seconds % 2 == 0 ? Colors.Yellow : Colors.Off;
        }

        private string GetHoursTop(int hours)
        {
           return string.Join("", GetLampStates(hours / 5, 4, Colors.Red));
        }

        private string GetHoursBottom(int hours)
        {
            return string.Join("", GetLampStates(hours % 5, 4, Colors.Red));
        }

        private string GetMinutesTop(int minutes)
        {
            return string.Join("", GetLampStates(minutes / 5, 11, Colors.Yellow)).Replace("YYY", "YYR");
        }

        private string GetMinutesBottom(int minutes)
        {
            return string.Join("", GetLampStates(minutes % 5, 4, Colors.Yellow));
        }
        
        private IList<string> GetLampStates(int activeLamps, int numberOfLamps, string color)
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
