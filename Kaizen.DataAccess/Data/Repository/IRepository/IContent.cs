using Kaizen.Models;

namespace Kaizen.DataAccess.Data.Repository.IRepository
{
    public interface IContent : IRepository<Content>
    {
        void update(Content content);
    }
}