using System.Collections.Generic;
using System.Linq;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaizen.DataAccess.Data.Repository
{
    public class ClassRepository : Repository<Class>, IClass
    {
        private readonly AppDbContext db;

        public ClassRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }
        public IEnumerable<SelectListItem> GetClassesForDropDown()
        {
            return db.Classes.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
        }

        public void Update(Class @class)
        {
            db.Classes.Update(@class);
        }
    }
}