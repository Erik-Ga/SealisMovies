using Microsoft.AspNetCore.Mvc;
using SealisMoviesApi.Data;
using SealisMoviesApi.Models;

namespace SealisMoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager _categoryManager;
        public CategoryController(CategoryManager categoryManager) 
        {
            _categoryManager = categoryManager;
        }


        [HttpGet]
        public async Task<List<Models.Category>> GetCategories() 
        {
            var categories = await _categoryManager.GetAllCategories();
            return categories;
        }

        [HttpGet("{id}")]
        public async Task<Models.Category> Get(int id)
        {
            var product = await _categoryManager.GetCategory(id);

            return product;
        }

        [HttpPost]
        public async Task Post([FromBody] Models.Category category)
        {
            await _categoryManager.AddCategory(category);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _categoryManager.DeleteCategory(id);
        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Models.Category category)
        {
            await _categoryManager.UpdateCategory(category, id);
        }
    }
}
