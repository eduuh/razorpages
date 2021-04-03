using Kaizen.DataAccess.Data.Repository.IRepository;

namespace Kaizen.DataAccess.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _db;
        public UnitofWork(AppDbContext db)
        {
            _db = db;
            School = new SchoolRepository(_db);
            Class = new ClassRepository(_db);
        }
        public ISchool School { get; private set; }
        public IClass Class { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}