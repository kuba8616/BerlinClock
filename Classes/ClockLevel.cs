using BerlinClock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Classes
{
    //tutaj zaczołem kombinować z klasa abstrakcyjna
    public abstract class ClockLevel
    {
        protected Func<int, int> conditionLevel1;
        protected Func<int, int> conditionLevel2;

        protected virtual List<string> GetLampState(int activeLamps, int lamps, string color)
        {
            //int activeLamps = func();
            var list = new List<string>();

            for (int i = 1; i <= lamps; i++)
            {
                list.Add((i <= activeLamps ? color : Colors.Off));
            }

            return list;
        }

        public virtual string GetTime(int hours)
        {
            var level1 = GetLampState(conditionLevel1(hours), 4, Colors.Red);
            var level2 = GetLampState(conditionLevel1(hours), 4, Colors.Red);
            level1.Add(Environment.NewLine);
            level1.AddRange(level2);
            return string.Join("", level1);
        }
    }

    public class HoursLevel : ClockLevel
    {
        protected new Func<int, int> conditionLevel1 = (hours) => { return hours / 5; };
        protected new Func<int, int> conditionLevel2 = (hours) => { return hours % 5; };
    }

    public class MinutesLevel : ClockLevel
    {
        protected new Func<int, int> conditionLevel1 = (minutes) => { return minutes / 5; };
        protected new Func<int, int> conditionLevel2 = (minutes) => { return minutes % 5; };
    }
}
