using System.IO;

namespace uploaddownloadfiles.Models
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