using System;
using ajf.ns_planner.shared2;

namespace ajf.ns_planner.shared
{
    public class CurrentProject : ObservableObject, ICurrentProject
    {
        private int _projectId;
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }

        public string DocumentDirectory
        {
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return path + "\\" + "NsPlanner";
            }
        }

        public int ProjectId
        {
            get { return _projectId; }
            set
            {
                _projectId = value;
                OnPropertyChanged();
            }
        }
    }
}