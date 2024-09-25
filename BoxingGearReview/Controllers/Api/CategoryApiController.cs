using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxingGearReview.Controllers.Api
{

    [ApiController]
    [Route("api/category")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryApiController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoriesDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _categoryRepository.GetCategory(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]  // Created
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(422)]  // Unprocessable Entity
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            // Check if the category already exists
            var categoryExists = _categoryRepository.GetCategories()
                .Any(c => c.Name.Trim().ToUpper() == categoryDto.Name.TrimEnd().ToUpper());

            if (categoryExists)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState); // Unprocessable Entity
            }

            // Mapping DTO to the Category model
            var category = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", "Something went wrong while saving the category");
                return StatusCode(500, ModelState); // Internal Server Error
            }

            return StatusCode(201, "Category created successfully");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest(ModelState);

            if (categoryId != categoryDto.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToUpdate = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.UpdateCategory(categoryToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}