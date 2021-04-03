using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;
using Kaizen.Utilities.Services;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.DataAccess.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _db;
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signinmanager;
        private readonly IJwtToken jwtgenerator;
        private readonly IUserAccessor userAccessor;
        private readonly RoleManager<IdentityRole> roleManager;

        public UnitofWork(AppDbContext db,
AppDbContext context,
            UserManager<AppUser> usermanager,
            SignInManager<AppUser> signinmanager,
            IJwtToken jwtgenerator,
            IUserAccessor userAccessor,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            this.context = context;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.jwtgenerator = jwtgenerator;
            this.userAccessor = userAccessor;
            this.roleManager = roleManager;
            School = new SchoolRepository(_db);
            Class = new ClassRepository(_db);
            AppUser = new AppUserRepository(_db, usermanager, signinmanager, jwtgenerator, userAccessor, roleManager);
        }
        public ISchool School { get; private set; }
        public IClass Class { get; private set; }
        public IAppUserRepository AppUser { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
            usermanager.Dispose();
            roleManager.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}