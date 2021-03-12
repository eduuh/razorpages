using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uploaddownloadfiles.Interface;

namespace uploaddownloadfiles.Controllers
{
    public class FilesController : Controller
    {
        public readonly IBlobService _blobservice;
        public FilesController(IBlobService blobservice)
        {
            _blobservice = blobservice;
        }

        [HttpGet("blob/{blobName}")]
        public async Task<IActionResult> GetBlob(string blobName) {
          var data = await _blobservice.GetBlobAsync(blobName);
            return File(data.Content, data.ContentType);
        }

        [HttpGet("blob/list")]
        public async Task<IActionResult> ListBlobs() {
            return Ok(await _blobservice.ListBlobAsync());
        }

        [HttpPost("blob/upload")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request) {
            await _blobservice.UploadFileBlobAsync(request.FilePath, request.FileName);
            return Ok();
        }

        [HttpPost("blob/uploadcontent")]
        public async Task<IActionResult> UploadContent([FromBody] UploadContentRequest request) {
            await _blobservice.UploadContentBlobAsync(request.Content, request.FileName);
            return Ok();
        }

        [HttpPost("blob/delete")]
        public async Task<IActionResult> DeleteBlock(string blobname) {
            await _blobservice.DeleteBlobAsync(blobname);
            return Ok();
        }
    }
}