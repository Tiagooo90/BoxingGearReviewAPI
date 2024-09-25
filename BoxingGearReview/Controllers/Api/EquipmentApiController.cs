using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using BoxingGearReview.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BoxingGearReview.Controllers.Api
{
    [ApiController]
    [Route("api/equipment")]
    public class EquipmentApiController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public EquipmentApiController(IEquipmentRepository equipmentRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _equipmentRepository = equipmentRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EquipmentDto>))]
        public IActionResult GetEquipments()
        {
            
            var equipmentDtos = _mapper.Map<List<EquipmentDto>>(_equipmentRepository.GetEquipments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(equipmentDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EquipmentDto))]
        [ProducesResponseType(404)]
        public IActionResult GetEquipment(int id)
        {
            if (!_equipmentRepository.EquipmentExists(id))
                return NotFound();

            var equipment = _equipmentRepository.GetEquipment(id);
            var equipmentDto = _mapper.Map<EquipmentDto>(equipment);

            equipmentDto.Reviews = _reviewRepository.GetReviewsByEquipment(id)
    .Select(r => _mapper.Map<ReviewDto>(r)).ToList();

            return Ok(equipmentDto);
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EquipmentDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetEquipmentsByCategory(int categoryId)
        {
            var equipments = _equipmentRepository.GetEquipmentsByCategory(categoryId);
            if (equipments == null || !equipments.Any())
                return NotFound();

            var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(equipments);
            return Ok(equipmentDtos);
        }

        [HttpGet("brand/{brandId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EquipmentDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetEquipmentsByBrand(int brandId)
        {
            var equipments = _equipmentRepository.GetEquipmentsByBrand(brandId);
            if (equipments == null || !equipments.Any())
                return NotFound();

            var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(equipments);
            return Ok(equipmentDtos);
        }

        // POST: api/Equipment
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateEquipment([FromBody] EquipmentDto equipmentDto)
        {
            if (equipmentDto == null)
                return BadRequest(ModelState);

            // Check if the equipment already exists
            var equipmentExists = _equipmentRepository.GetEquipments()
                .Any(e => e.Name.Trim().ToUpper() == equipmentDto.Name.TrimEnd().ToUpper());

            if (equipmentExists)
            {
                ModelState.AddModelError("", "Equipment already exists");
                return StatusCode(422, ModelState); // Unprocessable Entity
            }

            // Mapping DTO to the Equipment model
            var equipment = _mapper.Map<Equipment>(equipmentDto);

            if (!_equipmentRepository.CreateEquipment(equipment))
            {
                ModelState.AddModelError("", "Something went wrong while saving the equipment");
                return StatusCode(500, ModelState); // Internal Server Error
            }

            return StatusCode(201, "Equipment created successfully");
        }

        // PUT: api/Equipment/{equipmentId}
        [HttpPut("{equipmentId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult UpdateEquipment(int equipmentId, [FromBody] EquipmentDto equipmentDto)
        {
            if (equipmentDto == null)
                return BadRequest(ModelState);

            if (equipmentId != equipmentDto.Id)
                return BadRequest(ModelState);

            if (!_equipmentRepository.EquipmentExists(equipmentId))
                return NotFound();

            var equipmentToUpdate = _mapper.Map<Equipment>(equipmentDto);

            if (!_equipmentRepository.UpdateEquipment(equipmentToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating the equipment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Equipment/{equipmentId}
        [HttpDelete("{equipmentId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult DeleteEquipment(int equipmentId)
        {
            if (!_equipmentRepository.EquipmentExists(equipmentId))
                return NotFound();

            var equipmentToDelete = _equipmentRepository.GetEquipment(equipmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_equipmentRepository.DeleteEquipment(equipmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the equipment");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
