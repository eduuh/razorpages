namespace Kaizen.Utilities
{
    public class StorageHelper
    {
        //IsImage
    /*
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            var format = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            return format.Any(item => file.FileName.EndsWith(item, System.StringComparison.OrdinalIgnoreCase));
        }

        //UploadFileToStorage

        public static async Task<BlobContentInfo> UploadFileToStorage(Stream filestream, string filename, AzureStorageConfig _storageconfig)
        {
            var bloburl = new Uri($"https://{_storageconfig.AccountName}.blob.core.windows.net/{_storageconfig.ImageContainer}{filename}");

            var storagecred = new StorageSharedKeyCredential(_storageconfig.AccountName, _storageconfig.AccountKey);

            BlobClient blobclient = new BlobClient(bloburl, storagecred);
            var responce = await blobclient.UploadAsync(filestream);
            return responce;
        }

        //GetUrls

        public static async Task<List<string>> GetUrls(string scontainer) {
          List<string> urls = new List<string>();
          var accounturl = new Uri($"https://{_storageconfig.AccountName}.blob.core.windows.net");
          var blobclient = new BlobServiceClient(accounturl);

          var container = blobclient.GetBlobContainerClient(scontainer);
          if (container.Exists())
          {
            foreach (var blobitem in container.GetBlobs())
            {
              urls.Add($"{container.Uri}/{blobitem.Name}");
            }
          }
            return await Task.FromResult(urls);
        }

*/
}
}