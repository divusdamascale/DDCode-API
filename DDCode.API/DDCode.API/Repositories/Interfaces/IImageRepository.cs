using DDCode.API.Models.Domain;

namespace DDCode.API.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file,BlogImage image);
    }
}
