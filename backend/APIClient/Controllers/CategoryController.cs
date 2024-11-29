using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DTOs.Category;
using Repository.Models;
using Service.Interface;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryServices categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoriesReadDto = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
            return Ok(categoriesReadDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);
            return Ok(categoryReadDto);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var categoryModel = _mapper.Map<Category>(categoryCreateDto);
            var result = await _categoryService.CreateCategoryAsync(categoryModel);

            if (!result)
                return BadRequest("Failed to create category.");

            var categoryReadDto = _mapper.Map<CategoryReadDto>(categoryModel);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryReadDto.CategoryId }, categoryReadDto);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryCreateDto categoryUpdateDto)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            _mapper.Map(categoryUpdateDto, category);
            var result = await _categoryService.UpdateCategoryAsync(category);

            if (!result)
                return BadRequest("Failed to update category.");

            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
