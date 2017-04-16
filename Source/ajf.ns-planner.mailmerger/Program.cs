using System;
using ajf.ns_planner.shared2;
using AutoMapper;

namespace ajf.ns_planner.mailmerger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Mapper.CreateMap<IPlannerSettings, DerivedPlannerSettings>();

                //MailMergingService.DoMailMerge();
            }
            finally
            {
                Console.WriteLine("Tryk enter");
                Console.ReadLine();
            }
        }
    }
}