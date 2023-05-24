using Microsoft.AspNetCore.Mvc;
using SealisMoviesApi.Data;
using SealisMoviesApi.Models;

namespace SealisMoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager;
        public CategoryController(CategoryManager categoryManager) 
        {
            _categoryManager = categoryManager;
        }


        [HttpGet]
        public async Task <List<Models.Category>> Get() 
        {
            var categories = await _categoryManager.GetCategories();
            return categories;
        }

        [HttpPost]
        public async Task Post([FromBody] Models.Category category)
        {
            await _categoryManager.AddCategory(category);
        }
        [HttpDelete]
        public async Task Delete([FromBody] Models.Category category)
        {
            await _categoryManager.DeleteCategory(category);
        }
        [HttpPut]
        public async Task Put([FromBody] Models.Category category)
        {
            await _categoryManager.UpdateCategory(category);
        }
    }
}
