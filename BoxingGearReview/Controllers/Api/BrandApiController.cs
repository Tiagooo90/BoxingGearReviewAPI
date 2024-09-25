using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxingGearReview.Controllers.Api
{

    [ApiController]
    [Route("api/brand")]
    public class BrandApiController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandApiController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BrandDto>))]
        public IActionResult GetBrands()
        {
            var brands = _brandRepository.GetBrands();
            var brandDtos = _mapper.Map<IEnumerable<BrandDto>>(brands);
            return Ok(brandDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BrandDto))]
        [ProducesResponseType(404)]
        public IActionResult GetBrand(int id)
        {
            if (!_brandRepository.BrandExists(id))
                return NotFound();

            var brand = _brandRepository.GetBrand(id);
            var brandDto = _mapper.Map<BrandDto>(brand);

            return Ok(brandDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]  // Created
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(422)]  // Unprocessable Entity
        public IActionResult CreateBrand([FromBody] BrandDto brandDto)
        {
            if (brandDto == null)
                return BadRequest(ModelState);

            // Check if the brand already exists
            var brandExists = _brandRepository.GetBrands()
                .Any(b => b.Name.Trim().ToUpper() == brandDto.Name.TrimEnd().ToUpper());

            if (brandExists)
            {
                ModelState.AddModelError("", "Brand already exists");
                return StatusCode(422, ModelState); // Unprocessable Entity
            }

            // Mapping DTO to the Brand model
            var brand = _mapper.Map<Brand>(brandDto);

            if (!_brandRepository.CreateBrand(brand))
            {
                ModelState.AddModelError("", "Something went wrong while saving the brand");
                return StatusCode(500, ModelState); // Internal Server Error
            }

            return StatusCode(201, "Brand created successfully");
        }

        // PUT: api/Brand/{brandId}
        [HttpPut("{brandId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult UpdateBrand(int brandId, [FromBody] BrandDto brandDto)
        {
            if (brandDto == null)
                return BadRequest(ModelState);

            if (brandId != brandDto.Id)
                return BadRequest(ModelState);

            if (!_brandRepository.BrandExists(brandId))
                return NotFound();

            var brandToUpdate = _mapper.Map<Brand>(brandDto);

            if (!_brandRepository.UpdateBrand(brandToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating the brand");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Brand/{brandId}
        [HttpDelete("{brandId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult DeleteBrand(int brandId)
        {
            if (!_brandRepository.BrandExists(brandId))
                return NotFound();

            var brandToDelete = _brandRepository.GetBrand(brandId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_brandRepository.DeleteBrand(brandToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the brand");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}