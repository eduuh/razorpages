using Microsoft.AspNetCore.StaticFiles;

namespace Kaizen.Utilities
{ 
    public static class FileExtensions
    {
        public static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string filename) {
            if (!Provider.TryGetContentType(filename, out var contentType)) {
                contentType = "application/octet-stream";
            }
            return contentType;
        }



    }
}