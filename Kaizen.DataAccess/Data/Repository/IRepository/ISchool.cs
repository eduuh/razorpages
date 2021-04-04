using System.Collections.Generic;
using Kaizen.Models;
using Kaizen.Models.Models;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface ISchool : IRepository<School>
    {
        IEnumerable<SchoolName> GetSchooListForDropdown();
        void Update(School school);
    }
}