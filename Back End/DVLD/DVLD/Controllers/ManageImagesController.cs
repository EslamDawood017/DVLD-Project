using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManageImagesController : ControllerBase
    {
     
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No File Uploaded");

            var uploadDirectory = @$"{Environment.CurrentDirectory}\wwwroot\images\People\";

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            var filePath = Path.Combine(uploadDirectory, fileName);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Ok(new { filePath = $"images/People/{fileName}" });
        }
        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var UploadDirectory = @$"{Environment.CurrentDirectory}\wwwroot\images\People\";
            var FilePath = Path.Combine(UploadDirectory, fileName);

            if (!System.IO.File.Exists(FilePath))
                return NotFound("Image not Found");

            var image = System.IO.File.OpenRead(FilePath);
            var mimType = GetMimeType(FilePath);

            return File(image, mimType);
        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
        }
    }

