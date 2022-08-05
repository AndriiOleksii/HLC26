using System;
using System.IO;
using System.Threading.Tasks;
using WebAPILambda.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace WebAPILambda.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return $"Hello World from inside a lambda {DateTime.Now}";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"You asked for {id}";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileModel model)
        {
            var source = await Image.LoadAsync(model.File.OpenReadStream());

            var memoryStream = new MemoryStream();
            string contentType;

            switch (model.ConvertType)
            {
                case ConvertType.BMP:
                    await source.SaveAsBmpAsync(memoryStream);
                    contentType = "image/bmp";
                    break;
                case ConvertType.GIF:
                    await source.SaveAsGifAsync(memoryStream);
                    contentType = "image/gif";
                    break;
                case ConvertType.PNG:
                    await source.SaveAsPngAsync(memoryStream);
                    contentType = "image/png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (memoryStream.CanSeek)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
            }

            return File(memoryStream, contentType);
        }

    }
}
