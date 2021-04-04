using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models.Models;

namespace Kaizen.DataAccess.Data.Repository
{
    public class CultureReportRepository : Repository<CultureReport>, ICultureReport
    {
        public CultureReportRepository(AppDbContext context) : base(context)
        {

        }
        public void Update(CultureReport report)
        {
            var objecttoupdate = Get(report.Id);
            objecttoupdate.GenderAffected = report.GenderAffected;
            objecttoupdate.OccurrenceTimes = report.OccurrenceTimes;
            objecttoupdate.Region = report.Region;
        }
    }
}
