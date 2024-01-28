using DDCode.API.Data;
using DDCode.API.Migrations;
using DDCode.API.Models.Domain;
using DDCode.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDCode.API.Repositories.Implementation
{
    public class ImageRepository(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor) : IImageRepository
    {
        public async Task<IEnumerable<BlogImage>> GetImages()
        {
            return await context.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file,BlogImage image)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",$"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);

            await file.CopyToAsync(stream);
            var httpRequest = httpContextAccessor.HttpContext.Request;

            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.Url = urlPath;

            await context.BlogImages.AddAsync(image);
            await context.SaveChangesAsync();
            return image;
        }
    }
}
