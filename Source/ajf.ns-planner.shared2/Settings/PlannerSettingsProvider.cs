using System;
using System.IO;
using ajf.ns_planner.shared2.Interfaces;

namespace ajf.ns_planner.shared2.Settings
{
    public class PlannerSettingsProvider : IPlannerSettingsProvider
    {
        private readonly IMyConfigurationManager _myConfigurationManager;
        private readonly INsContext _nsContext;
        private readonly ILogItemListViewModel _logItemListViewModel;

        public PlannerSettingsProvider(IMyConfigurationManager myConfigurationManager, INsContext nsContext, ILogItemListViewModel logItemListViewModel)
        {
            _myConfigurationManager = myConfigurationManager;
            _nsContext = nsContext;
            _logItemListViewModel = logItemListViewModel;
        }

        public void Check(string fullPath, string subject)
        {
            Console.WriteLine(subject + " læses fra " + fullPath);
            if (File.Exists(fullPath))
            {
                Console.WriteLine("-- Bekræftet: Fil findes: " + fullPath);
            }
            else
            {
                Console.WriteLine("-- Advarsel: Fil ikke fundet: " + fullPath);
            }
        }

        public IDerivedPlannerSettings GetDerivedPlannerSettings(bool printFileExistsResult)
        {
            var directory = _nsContext.Directory;
            var configFile = _nsContext.ConfigFile;
            if (directory == null)
            {
                throw new Exception("Directory er ikke sat. Programmet kan ikke køre.");
            }
            if (configFile== null)
            {
                throw new Exception("ConfigFile er ikke sat. Programmet kan ikke køre.");
            }

            var plannerSettings = _myConfigurationManager.GetSettings(directory, configFile);
            var derivedPlannerSettings = new DerivedPlannerSettings(plannerSettings);
            
            Console.WriteLine("------");
            Console.WriteLine("Klar til flet i {0}", directory);

            if (printFileExistsResult)
            {
                Check(derivedPlannerSettings.RequestFileFullPath, "Ønsker");
                Check(derivedPlannerSettings.CounsellorFileFullPath, "Vejledere");
                Check(derivedPlannerSettings.EventFileFullPath, "Arrangementer");
                Check(derivedPlannerSettings.PlaceFileFullPath, "Steder");
                Check(derivedPlannerSettings.DestinationFileFullPath, "Fletteresultat");
            }
            return derivedPlannerSettings;
        }
    }
}