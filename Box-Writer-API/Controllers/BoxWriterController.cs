using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Box_Writer_API.Controllers
{
    [Produces("application/json")]
    [Route("api/BoxWriter")]
    public class BoxWriterController : Controller
    {
        // GET api/boxwriter/
        [HttpGet]
        public IActionResult Get(String word, String label, Int32 spacing = 2, Int32 offset = 10)
        {
            if(word == null)
            {
                return Content("INVALID BOX: No Word Specified!");
            }

            if(label == null)
            {
                label = String.Empty;
            }

            BoxWriter _BoxWriter = new BoxWriter(word, label);
            _BoxWriter.SetSpacing(spacing);
            _BoxWriter.SetOffset(offset);

            _BoxWriter.Boxify();

            return Content(_BoxWriter.ToString());
        }
    }
}