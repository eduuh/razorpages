using System.Collections.Generic;
using System.Threading.Tasks;
using uploaddownloadfiles.Models;

namespace uploaddownloadfiles.Interface
{
    public interface IBlobService
    {
        public Task<IEnumerable<string>> ListBlobAsync();
        public Task UploadFileBlobAsync(string filepath, string filename);
        public Task UploadContentBlobAsync(string content, string filename);
        public Task DeleteBlobAsync(string blobName);
        Task<BlobInformation> GetBlobAsync(string blobname);
    }
}