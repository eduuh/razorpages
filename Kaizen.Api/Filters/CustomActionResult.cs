using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UploadandDowloadService.Filters
{
    public class CustomActionResult<T> : IActionResult
    {
        private readonly T data;
        private readonly HttpStatusCode statuscode;

        public CustomActionResult(T data, HttpStatusCode statuscode)
        {
            this.data = data;
            this.statuscode = statuscode;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(data);
            objectResult.StatusCode = (int)statuscode;
            await objectResult.ExecuteResultAsync(context);
        }
    }
}