using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using uploaddownloadfiles.Helper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using UploadandDowloadService.Models;
using UploadandDowloadService.Interface;

namespace UploadandDowloadService.Infratructure
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobService(BlobServiceClient blobserviceclient)
        {
            _blobServiceClient = blobserviceclient;
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerclient = _blobServiceClient.GetBlobContainerClient("files");
            var blobclient = containerclient.GetBlobClient(blobName);
            await blobclient.DeleteIfExistsAsync();
        }

        public async Task<BlobInformation> GetBlobAsync(string blobname)
        {
            var containerclient = _blobServiceClient.GetBlobContainerClient("files");
            var blobclient = containerclient.GetBlobClient(blobname);
            var blobdownloadinfo = await blobclient.DownloadAsync();
            return new BlobInformation(
              blobdownloadinfo.Value.Content,
              blobdownloadinfo.Value.ContentType);
        }

        public async Task<IEnumerable<string>> ListBlobAsync()
        {
            var containerclient = _blobServiceClient.GetBlobContainerClient("files");
            var items = new List<string>();

            await foreach (var blobitem in containerclient.GetBlobsAsync()) {
                items.Add(blobitem.Name);
            }
            return items;
        }

        public async Task UploadContentBlobAsync(string content, string filename)
        {
            var containerclient = _blobServiceClient.GetBlobContainerClient("files");
            var blobclient = containerclient.GetBlobClient(filename);
            var bytes = Encoding.UTF8.GetBytes(content);
            await using var memorystream = new MemoryStream(bytes);
            await blobclient.UploadAsync(memorystream, new BlobHttpHeaders { ContentType = filename.GetContentType() });
        }

        public async Task UploadFileBlobAsync(string filepath, string filename)
        {
            var containerclient = _blobServiceClient.GetBlobContainerClient("files");
            var blobclient = containerclient.GetBlobClient(filename);
            await blobclient.UploadAsync(filepath, new BlobHttpHeaders { ContentType = filepath.GetContentType() });
        }
    }
}