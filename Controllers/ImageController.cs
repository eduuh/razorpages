using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using uploaddownloadfiles.Models;

namespace uploaddownloadfiles.Controllers
{
    public class ImageController: BaseController
    {
        public ImageController(IOptions<AzureStorageConfig> config): base(config)
        {
            
        }
        //upload file Image with metadata

        // getUrls
        // getUrlsfora specific metadata
    }
}