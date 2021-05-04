using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UserController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "AllUsers")]
        public IActionResult GetUsers()
        {
            var users = _repository.User.AllUsers(changes: false);

            var userDto = _mapper.Map<IEnumerable<Users>>(users);

            return Ok(userDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                var user = _repository.User._user(id, changes: false);
                if (user == null)
                {
                    _logger.LogError("User has not been set");
                    return NotFound();
                }
                else
                {
                    var userDto = _mapper.Map<Entities.Models.User>(user);
                    return Ok(userDto);

                }
            }
            catch (Exception e)
            {
                _logger.LogError("User has not been set");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}", Name = "GetUser")]
        public IActionResult createUser([FromBody] UserCreation user)
        {
            if (user == null)
            {
                _logger.LogError("User has not been set");
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State");
                return UnprocessableEntity(ModelState);
            }

            var _user = _mapper.Map<Entities.Models.User>(user);

            _repository.User.CreateUser(_user);
            _repository.Save();

            var returnUser = _mapper.Map<User>(_user);

            return CreatedAtRoute("GetUser", new {id = returnUser.Id}, returnUser);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserUpdate user)
        {
            if (user == null)
            {
                _logger.LogError("User has not been set");
                return BadRequest("User has not been set");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State");
                return UnprocessableEntity(ModelState);
            }

            var _user = _mapper.Map<Entities.Models.User>(user);
            if (_user == null)
            {
                _logger.LogInfo("Organization with that ID does not exist");
                return NotFound();
            }

            _mapper.Map(user, _user);
            _repository.Save();

            return NoContent();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var _user = _repository.User._user(id, changes: false);

            if (_user == null)
            {
                _logger.LogInfo("Organization with that ID does not exist");
                return NotFound();
            }

            _repository.User.DeleteUser(_user);
            _repository.Save();

            return NoContent();
        }
    }
}
