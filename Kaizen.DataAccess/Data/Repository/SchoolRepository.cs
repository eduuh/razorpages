using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
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
        public IEnumerable<SelectListItem> GetSchooListForDropdown()
        {
            return _context.Schools.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(School school)
        {
            var objFromDb = _context.Schools.FirstOrDefault(s => s.Id == school.Id);
            objFromDb.Name = school.Name;
            objFromDb.Motto = school.Motto;
            objFromDb.image = school.image;
            if (objFromDb.ContactId == null)
            {
                objFromDb.ContactId = school.ContactId;
            }
            _context.SaveChanges();
            // objFromDb.Contact.Address = 
        }
    }
}