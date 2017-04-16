using System;
using System.Collections.Generic;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public class DateViewModel
    {
        public DateTime DateTime { get; private set; }
        public IEnumerable<CounsellorViewModel> CounsellorViewModels { get; private set; }

        public DateViewModel(DateTime dateTime, IEnumerable<CounsellorViewModel> counsellorViewModels)
        {
            DateTime = dateTime;
            CounsellorViewModels = counsellorViewModels;
        }
    }
}