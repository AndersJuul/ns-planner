using System;
using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.servicelayer.Models;

namespace ajf.ns_planner.viewmodels.ViewModels
{
    public class EditCounsellorDatesViewModel
    {
        private readonly Dictionary<DateTime, DateViewModel> _dateViewModels;

        public EditCounsellorDatesViewModel()
        {
            _dateViewModels = new Dictionary<DateTime, DateViewModel>();
        }

        public string Title
        {
            get { return "Redigering af rådighedsdatoer"; }
        }

        public IEnumerable<DateViewModel> SortedDateList
        {
            get { return from dvm in _dateViewModels orderby dvm.Key select dvm.Value; }
        }

        public void Add(DateViewModel dateViewModel)
        {
            _dateViewModels.Add(dateViewModel.DateTime, dateViewModel);
        }

        public void Check(DateTime date, int counsellorId)
        {
            if (!_dateViewModels.ContainsKey(date))
                return;

            var dateViewModel = _dateViewModels[date];
            var counsellorViewModel = dateViewModel.CounsellorViewModels.SingleOrDefault(x => x.CounsellorId == counsellorId);
            if (counsellorViewModel == null)
                return;

            counsellorViewModel.IsChecked = true;
        }

        public IEnumerable<CounsellorDate> GetCounsellorDates(int projectId)
        {
            var counsellorDates = new List<CounsellorDate>();
            foreach (var dateViewModel in _dateViewModels.Values)
            {
                counsellorDates
                    .AddRange(from counsellorViewModel in dateViewModel.CounsellorViewModels
                        where counsellorViewModel.IsChecked
                        select GetCounsellorDate(dateViewModel, counsellorViewModel,projectId));
            }

            return counsellorDates;
        }

        private static CounsellorDate GetCounsellorDate(DateViewModel dateViewModel, CounsellorViewModel counsellorViewModel, int projectId)
        {
            var counsellorDate = new CounsellorDate()
            {
                Date = dateViewModel.DateTime,
                CounsellorId = counsellorViewModel.CounsellorId,
                ProjectId = projectId
            };
            //AutoMapper.Mapper.Map(counsellorViewModel, counsellorDate);
            return counsellorDate;
        }
    }
}