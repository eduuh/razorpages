using System.IO;

namespace UploadandDowloadService.Models
{
    public class BlobInformation
    {
        public string ContentType;
        public Stream Content;
        public BlobInformation(Stream Content, string ContentType)
        {
            this.Content = Content;
            this.ContentType = ContentType;

        }
    }
}