using Contracts;
using CsvHelper;
using Entities.Query;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVReader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditorController : Controller
    {
        private ILogger<FilesController> _logger;
        ICsvService _service;
        public EditorController(ILogger<FilesController> logger, ICsvService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [EnableCors("Policy1")]
        [Route("getdata")]
        public async Task<ActionResult> GetData(QueryGetData data)
        {
            var result = _service.GetData(data);
            return Ok(result);
        }

        /*        [HttpPost]
                [EnableCors("Policy1")]
                [Route("modify")]
                public async Task<ActionResult> Modify(ListQueryModifyData data)
                {
                    var result = _reader.ModifyData(data);
                    return Ok(result);
                }

                [HttpPost]
                [EnableCors("Policy1")]
                [Route("encoding")]
                public async Task<ActionResult> SetEncoding([FromForm] QueryEncodingData data)
                {
                    var result = _reader.SetEncoding(data);
                    if (result)
                        return Ok(result);
                    return BadRequest();
                }*/

    }
}
