using System.Collections.Generic;
using Kaizen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface ISchool : IRepository<School>
    {
        IEnumerable<SelectListItem> GetSchooListForDropdown();
        void Update(School school);
    }
}