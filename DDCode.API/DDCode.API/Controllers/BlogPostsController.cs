﻿using DDCode.API.Models.Domain;
using DDCode.API.Models.DTO;
using DDCode.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IBlogPostRepository _blogpostRepository,ICategoryRepository _categoryRepository) : ControllerBase
    {
        //POST : api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDTO request)
        {
            var blogpost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryId in request.Categories)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category is not null)
                {
                    blogpost.Categories.Add(category);
                }
            }

            await _blogpostRepository.CreateAsync(blogpost);

            var response = new BlogPostDTO
            {
                Id = blogpost.Id,
                Author = blogpost.Author,
                Content = blogpost.Content,
                FeaturedImageUrl = blogpost.FeaturedImageUrl,
                IsVisible = blogpost.IsVisible,
                PublishedDate = blogpost.PublishedDate,
                ShortDescription = blogpost.ShortDescription,
                Title = blogpost.Title,
                UrlHandle = blogpost.UrlHandle,
                Categories = blogpost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        //GET: /api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogposts = await _blogpostRepository.GetAllAsync();

            var response = new List<BlogPostDTO>();

            foreach (var blogpost in blogposts)
            {
                response.Add(
                    new BlogPostDTO
                    {
                        Id = blogpost.Id,
                        Author = blogpost.Author,
                        Content = blogpost.Content,
                        FeaturedImageUrl = blogpost.FeaturedImageUrl,
                        IsVisible = blogpost.IsVisible,
                        PublishedDate = blogpost.PublishedDate,
                        ShortDescription = blogpost.ShortDescription,
                        Title = blogpost.Title,
                        UrlHandle = blogpost.UrlHandle,

                    }
                );
            }

            return Ok(response);
        }
    }
}
