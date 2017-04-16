using System;
using System.IO;
using System.Reflection;
using ajf.ns_planner.shared2;
using AutoMapper;

namespace ajf.ns_planner.qf_console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Mapper.CreateMap<IPlannerSettings, DerivedPlannerSettings>();

            var location = Assembly.GetExecutingAssembly().Location;
            var directoryName = Path.GetDirectoryName(location);

            Console.WriteLine("Fletteprogram til arrangementplanlægning.");
            Console.WriteLine("-----");
            Console.WriteLine("Indstillinger hentes fra samme folder som programmet.");
            Console.WriteLine("Dvs: " + directoryName);

            do
            {
                //MergeService.MergeOnce(null);
            } while (true);
        }
    }
}