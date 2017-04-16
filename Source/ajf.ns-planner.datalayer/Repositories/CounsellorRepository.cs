using System.Collections.Generic;
using System.Linq;
using ajf.ns_planner.datalayer.Models;

namespace ajf.ns_planner.datalayer.Repositories
{
    public class CounsellorRepository : BaseRepository, ICounsellorRepository
    {
        public IEnumerable<Counsellor> ReadCounsellors(string filename)
        {
            var worksheet = GetFirstSheet(filename);
            if (worksheet == null)
            {
                return null;
            }

            var result = new List<Counsellor>();
            for (var i = worksheet.FirstRowNum + 1; i <= worksheet.LastRowNum; i++)
            {
                var row = worksheet.GetRow(i);

                var fullName = row.GetCell(0).StringCellValue;
                var phone = GetCellString(row, 1);
                var email = GetCellString(row, 2);
                var initials = GetCellString(row, 3);

                var counsellor = new Counsellor()
                {
                    FullName = fullName,
                    Phone = phone,
                    Email = email,
                    Initials = initials
                };
                result.Add(counsellor);
            }
            return result;
        }

        public IEnumerable<Counsellor> GetCounsellors(UnitOfWork unitOfWork, int projectId)
        {
                return unitOfWork.Db.Counsellors.Where(x => x.Project.Id == projectId).ToList();
        }
    }
}