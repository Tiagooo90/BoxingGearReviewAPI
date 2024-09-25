using AutoMapper;
using BoxingGearReview.Dto;
using BoxingGearReview.Interfaces;
using BoxingGearReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxingGearReview.Controllers.Api
{

    [ApiController]
    [Route("api/user")]
    public class UserApiController : ControllerBase

    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserApiController(IUserRepository UserRepository, IMapper mapper)
        {
            _userRepository = UserRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
                return NotFound();

            // Obtém o usuário do repositório
            var user = _userRepository.GetUser(id);

            if (user == null)
                return NotFound();

            // Mapeia o usuário para o DTO
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpGet("{userId}/rating")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]

        public IActionResult GetUserRating(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var rating = _userRepository.GetUserRating(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);

        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);

        }

        [HttpPost]
        [ProducesResponseType(204)]//204 for NoContent
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            // Verifica se o email já existe no banco de dados
            var existingUser = _userRepository.GetUserByEmail(userCreate.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "User with this email already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapeia o UserDto para a entidade User
            var userMap = _mapper.Map<User>(userCreate);

            // Adiciona o usuário no banco de dados
            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("User successfully created");
        }
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting the user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


    }
}
