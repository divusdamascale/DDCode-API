using DDCode.API.Models.Domain;
using DDCode.API.Models.DTO;
using DDCode.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImageRepository imageRepository) : ControllerBase
    {
        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile file,
            [FromForm] string fileName,
            [FromForm] string title
        )
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    DateCreated = DateTime.Now,
                    FileName = fileName,
                    Title = title,
                    FileExtension = Path.GetExtension(file.FileName).ToLower()
                };
                await imageRepository.Upload(file, blogImage);

                var respone = new BlogImageDTO
                {
                    Url = blogImage.Url,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Id = blogImage.Id
                };

                return Ok(respone);
            }
            return BadRequest();
        }

        //GET : /api/Images
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await imageRepository.GetImages();
            var response = images.Select(i => new BlogImageDTO
            {
                Url = i.Url,
                Title = i.Title,
                DateCreated = i.DateCreated,
                FileExtension = i.FileExtension,
                FileName = i.FileName,
                Id = i.Id
            });
            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            //todo: array in appsettings
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
            {
                ModelState.AddModelError("File", "Invalid file extension.");
            }

            if (file.Length > 1048576) //10mb
            {
                ModelState.AddModelError("File", "The file is too large. MAX:10mb");
            }
        }
    }
}
