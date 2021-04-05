using System;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISchool School { get; }
        IClass Class { get; }
        ICultureReport CultureReport { get; }
        IContent Content { get; }
        IAppUserRepository AppUser { get; }

        void Save();

    }
}