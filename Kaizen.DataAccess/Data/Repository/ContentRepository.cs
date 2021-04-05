using System.Linq;
using Kaizen.DataAccess.Data.Repository.IRepository;
using Kaizen.Models;

namespace Kaizen.DataAccess.Data.Repository
{
    public class ContentRepository : Repository<Content>, IContent
    {
        private readonly AppDbContext _context;
        public ContentRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public void update(Content content)
        {
            var objFromDb = _context.Contents.FirstOrDefault(s => s.Id == content.Id);
            objFromDb.Name = content.BlobUrl == null ? objFromDb.BlobUrl : content.BlobUrl;
            objFromDb.Name = content.ContentType == null ? objFromDb.ContentType : content.ContentType;
            objFromDb.Name = content.Description == null ? objFromDb.Description : content.Description;
            objFromDb.Name = content.Name == null ? objFromDb.Name : content.Name;
            objFromDb.Name = content.Region == null ? objFromDb.Region : content.Region;
            objFromDb.Name = content.Type == null ? objFromDb.Type : content.Type;
        }
    }
}