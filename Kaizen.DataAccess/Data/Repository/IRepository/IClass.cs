using System.Collections.Generic;
using Kaizen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IClass : IRepository<Class>
    {
        void Update(Class entity);
        IEnumerable<SelectListItem> GetClassesForDropDown();
    }
}