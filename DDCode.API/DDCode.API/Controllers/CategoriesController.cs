using DDCode.API.Data;
using DDCode.API.Models.Domain;
using DDCode.API.Models.DTO;
using DDCode.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDCode.API.Controllers
{
    //https://localhost:xxxx/api/Categories
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryRequestDTO request)
        {
            //Map DTO to Domain Model
            //TODO: create service for categories
            //TODO: create mapping method
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            //TODO:handle exceptions
            
            await _categoryRepository.CreateAsync(category);

            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };  
           
            return Ok(response);
        }
    }
}
