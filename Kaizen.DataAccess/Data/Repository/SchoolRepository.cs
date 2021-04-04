using System.Collections.Generic;
using System.Linq;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Kaizen.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.DataAccess.Data.Repository
{
    public class SchoolRepository : Repository<School>, ISchool
    {
        private readonly AppDbContext _context;

        public SchoolRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }
        public IEnumerable<SchoolName> GetSchooListForDropdown()
        {
            return _context.Schools.Select(i => new SchoolName()
            {
                Name = i.Name,
            });
        }

        public void Update(School school)
        {
            var objFromDb = _context.Schools.FirstOrDefault(s => s.Id == school.Id);
            objFromDb.Name = school.Name;
            objFromDb.Motto = school.Motto;
            objFromDb.image = school.image;
            objFromDb.Website = school.Website;
            objFromDb.image = school.image;
        }


    }
}