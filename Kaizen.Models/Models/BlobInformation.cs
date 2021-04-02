using System.IO;

namespace Kaizen.Models
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