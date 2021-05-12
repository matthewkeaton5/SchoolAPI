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
using ActionFilters;
using Entities.RequestFeatures;
using Newtonsoft.Json;

namespace SchoolAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]

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
        [HttpGet(Name = "getAllUsers")]
        public IActionResult GetUsers([FromQuery] UserParameters userParameters)
        {
            var usersFromDb = _repository.User.GetAllUsers(userParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(usersFromDb.MetaData));

            var userDto = _mapper.Map<IEnumerable<UsersDto>>(usersFromDb);

            return Ok(userDto);

        }
        [HttpGet("{id}", Name = "getUserById")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult GetUser(Guid id)
        {

            var user = HttpContext.Items["user"] as User;

            var userDto = _mapper.Map<UsersDto>(user);
            return Ok(userDto);


        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult UpdateUser(Guid id, [FromBody] UserUpdateDto user)
        {

            var userEntity = HttpContext.Items["user"] as User;

            _mapper.Map(user, userEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public IActionResult DeleteUser(Guid id)
        {
            var user = HttpContext.Items["user"] as User;

            _repository.User.DeleteUser(user);
            _repository.Save();

            return NoContent();
        }




    }
}