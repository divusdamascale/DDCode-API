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
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            //Map DTO to Domain Model
            //TODO: create service for categories
            //TODO: create mapping method
            var category = new Category { Name = request.Name,UrlHandle = request.UrlHandle };
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

        //GET: /api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var response = new List<CategoryDTO>();

            foreach(var category in categories)
            {
                response.Add(
                    new CategoryDTO
                    {
                        Id = category.Id,
                        Name = category.Name,
                        UrlHandle = category.UrlHandle
                    }
                );
            }

            return Ok(response);
        }

        //GET: /api/Categories/{id}
        [HttpGet]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if(category is null)
            {
                return NotFound();
            }
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }

        //PUT: /api/Categories/{id}
        [HttpPut]
        [Route("{categoryId:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid categoryId,[FromBody] UpdateCategoryRequestDTO request)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if(category is null)
            {
                return NotFound();
            }

            category.Name = request.Name;
            category.UrlHandle = request.UrlHandle;

            await _categoryRepository.UpdateAsync(category);

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
