using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreAsureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsureBlobController : ControllerBase
    {

        public AzureBlobService _service;
        public AsureBlobController(AzureBlobService _azservice)
        {
            _service = _azservice;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List <IFormFile>files)
        {
            var response = await _service.UploadFiles(files);
            return Ok(response);
        }

        [HttpGet("BlobList")]
        public async Task<IActionResult>GetBlobList()
        {
            var response =  await _service.GetUploadedBlob();
            return Ok(response);
        }

    }
}
