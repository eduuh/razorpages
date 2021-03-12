using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using uploaddownloadfiles.Models;

namespace uploaddownloadfiles.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IOptions<AzureStorageConfig> config = null;
        public BaseController(IOptions<AzureStorageConfig> config)
        {
            this.config = config;
        }

    }
}