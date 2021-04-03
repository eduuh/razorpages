using System;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        ISchool School { get; }
        IClass Class { get; }

        void Save();

    }
}