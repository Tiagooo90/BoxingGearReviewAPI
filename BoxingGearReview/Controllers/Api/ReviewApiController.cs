using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxingGearReview.Controllers.Api
{

    [ApiController]
    [Route("api/review")]
    public class ReviewApiController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewApiController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();
            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(404)]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id))
                return NotFound();

            var review = _reviewRepository.GetReview(id);
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Ok(reviewDto);
        }

        [HttpGet("equipment/{equipmentId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByEquipment(int equipmentId)
        {
            var reviews = _reviewRepository.GetReviewsByEquipment(equipmentId);
            if (reviews == null || !reviews.Any())
                return NotFound();

            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetReviewsByUser(int userId)
        {
            var reviews = _reviewRepository.GetReviewsByUser(userId);
            if (reviews == null || !reviews.Any())
                return NotFound();

            var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public IActionResult CreateReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest("Review data is required.");

            // Check if the review already exists
            var reviewExists = _reviewRepository.GetReviews()
                .Any(r => r.UserId == reviewDto.UserId && r.EquipmentId == reviewDto.EquipmentId);

            if (reviewExists)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState); // Unprocessable Entity
            }

            // Mapping DTO to the Review model
            var review = _mapper.Map<Review>(reviewDto);

            if (!_reviewRepository.CreateReview(review))
            {
                ModelState.AddModelError("", "Something went wrong while saving the review");
                return StatusCode(500, ModelState); // Internal Server Error
            }

            return StatusCode(201, "Review created successfully");
        }


        // PUT: api/Review/{reviewId}
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return BadRequest(ModelState);

            if (reviewId != reviewDto.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToUpdate = _mapper.Map<Review>(reviewDto);
            reviewToUpdate.Id = reviewId;  // Ensure the ID is set correctly

            if (!_reviewRepository.UpdateReview(reviewToUpdate))
            {
                ModelState.AddModelError("", "Something went wrong updating the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Review/{reviewId}
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]  // No Content
        [ProducesResponseType(400)]  // BadRequest
        [ProducesResponseType(404)]  // NotFound
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
