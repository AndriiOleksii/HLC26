using Microsoft.AspNetCore.Http;

namespace WebAPILambda.Models
{
    public class FileModel
    {
        public IFormFile File { get; set; }
        public ConvertType ConvertType { get; set; }
    }

    public enum ConvertType
    {
        PNG,
        GIF,
        BMP
    }
}
