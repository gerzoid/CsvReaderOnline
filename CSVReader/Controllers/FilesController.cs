using Contracts;
using Entities.Other;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVReader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private ILogger<FilesController> _logger;
        ICsvService _service;

        public FilesController(ILogger<FilesController> logger, ICsvService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [EnableCors("Policy1")]
        [Route("download")]
        public IActionResult DownloadFile([FromForm] string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", fileName.ToString());
            var fs = new FileStream(path, FileMode.Open);
            return File(fs, "application/octet-stream", fileName);
        }

        [HttpPost]
        [EnableCors("Policy1")]
        [RequestSizeLimit(50_000_000)]
        //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<ActionResult> Upload([FromForm] FileModel file)
        {
            try
            {
                var info = await _service.UploadFile(file);
                return StatusCode(StatusCodes.Status201Created, info);
            }
            catch (Exception E)
            {
                Console.WriteLine(E.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [EnableCors("Policy1")]
        [Route("open")]
        public async Task<ActionResult> Open([FromForm] string fileId)
        {
            var info = _service.OpenFile(fileId);
            return StatusCode(StatusCodes.Status201Created, info);
        }
    }

}