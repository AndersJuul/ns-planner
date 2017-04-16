using System;

namespace ajf.ns_planner.shared2.BasicMerge
{
    public class Statistics
    {
        public int RowsWithCounsellor { get; set; }
        public int RowsWithoutCounsellor { get; set; }
        public int RowsWithFutureDate { get; set; }

        public int Group
        {
            get
            {
                int dummy;
                return Math.DivRem(RowsWithCounsellor, 10, out dummy);
            }
        }
        
        public override string ToString()
        {
            return
                "-- Ønsker imødekommet:      " + RowsWithCounsellor + Environment.NewLine +
                "-- Ønsker i fremtiden:      " + RowsWithFutureDate + Environment.NewLine +
                "-- Ønsker ikke imødekommet: " + RowsWithoutCounsellor + Environment.NewLine;
        }
    }
}