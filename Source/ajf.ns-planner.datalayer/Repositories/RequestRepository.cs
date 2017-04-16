using System.Collections.Generic;
using ajf.ns_planner.datalayer.Models;
using NPOI.SS.UserModel;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        public IEnumerable<Request> ReadRequests(string filename)
        {
            var worksheet = GetFirstSheet(filename);
            if (worksheet == null)
            {
                return null;
            }

            var result = new List<Request>();
            for (var i = worksheet.FirstRowNum + 1; i <= worksheet.LastRowNum; i++)
            {
                var row = worksheet.GetRow(i);

                var requestTime = row.GetCell(0).DateCellValue;
                var contactName = GetCellString(row, 1);
                var contactPhone = GetCellString(row, 2);
                var contactEmail = GetCellString(row, 3);
                var applicantName = GetCellString(row, 4);

                var participantGroup = GetCellString(row, 5);
                var participantAge = GetCellString(row, 6);
                var participantNumber = GetCellString(row, 7);
                var requestedEvent = GetCellString(row, 8);
                var requestedMeetingPlace = GetCellString(row, 9);
                var requestedDate = GetDateString(row, 10);// GetCellString(row, 10);
                var comments = GetCellString(row, 11);
                var requestedPeriod = GetCellString(row, 12);
                
                var request = new Request
                {
                    RequestTime = requestTime,
                    ContactName = contactName,
                    ContactPhone = contactPhone,
                    ContactEmail = contactEmail,
                    ApplicantName = applicantName,
                    ParticipantGroup = participantGroup,
                    ParticipantAge = participantAge,
                    ParticipantNumber = participantNumber,
                    RequestedEvent = requestedEvent,
                    RequestedMeetingPlace = requestedMeetingPlace,
                    RequestedDate = requestedDate,
                    RequestedPeriod = requestedPeriod,
                    Comments = comments
                };
                
                result.Add(request);
            }
            return result;
        }

        private static string GetDateString(IRow row, int cellnum)
        {
            var cell = row.GetCell(cellnum);
            if (cell == null) return "";
            if (cell.CellType == CellType.String) return cell.StringCellValue;
            if(cell.CellType==CellType.Numeric)return cell.DateCellValue.ToString("yyyy-MM-dd");
            return "--IF--";
        }

        public IEnumerable<Request> ReadRequests(UnitOfWork unitOfWork)
        {
            return unitOfWork.Db.Requests;
        }
    }
}