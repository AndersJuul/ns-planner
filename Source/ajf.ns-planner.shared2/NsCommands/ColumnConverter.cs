using System;
using System.Linq;

namespace ajf.ns_planner.shared2.NsCommands
{
    public class ColumnConverter : IColumnConverter
    {
        public int ConvertToIndex(string columnName)
        {
            var numbers = columnName.Select(c => c - 64).ToList();
            numbers.Reverse();

            var result = 0;
            for (var i = 0; i < numbers.Count; i++)
            {
                var mul =Math.Pow(26,i);
                result +=Convert.ToInt16( mul * numbers[i]);
            }

            return result-1;
        }
    }
}