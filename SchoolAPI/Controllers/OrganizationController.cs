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
using Organization = Entities.DataTransferObjects.Organization;

namespace SchoolAPI.Controllers
{
    [Route("api/v1/organizations")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OrganizationsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllOrganizations")]
        public IActionResult GetOrganizations()
        {
            var organizations = _repository.Organization.GetAllOrganizations(trackChanges: false);

            var organizationDto = _mapper.Map<IEnumerable<Entities.DataTransferObjects.Organization>>(organizations);
            //uncomment the code below to test the global exception handling
            //throw new Exception("Exception");
            return Ok(organizationDto);
        }

        [HttpGet("{id}", Name = "GetOrganizations")]
        public IActionResult GetOrganizations(Guid id)
        {
            var organization = _repository.Organization.GetOrganization(id, trackChanges: false);
            if (organization == null)
            {
                _logger.LogError("Organization has not been set");
                return NotFound();
            }
            else
            {
                var organizationDto = _mapper.Map<Entities.DataTransferObjects.Organization>(organization);
                return Ok(organizationDto);

            }
        }

        [HttpPost(Name = "CreateOrganization")]
        public IActionResult CreateOrganization([FromBody] OrganizationCreation organization)
        {
            if (organization == null)
            {
                _logger.LogError("Organization has not been set");
                return BadRequest("Organization has not been set");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State");
                return UnprocessableEntity(ModelState);
            }

            var _organization = _mapper.Map<Entities.Models.Organization>(organization);

            _repository.Organization.createOrg(_organization);
            _repository.Save();

            var returnOrganization = _mapper.Map<Entities.DataTransferObjects.Organization>(_organization);

            return CreatedAtRoute("getOrganizationByID", new {Id = returnOrganization.Id}, returnOrganization);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrganization(Guid id, [FromBody] OrganizationUpdate organization)
        {
            if (organization == null)
            {
                _logger.LogError("Organization has not been set");
                return BadRequest("Organization has not been set");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State");
                return UnprocessableEntity(ModelState);
            }

            var _organization = _repository.Organization.GetOrganization(id, trackChanges: true);
            if (_organization == null)
            {
                _logger.LogInfo("Organization with that ID does not exist");
                return NotFound();
            }

            _mapper.Map(organization, _organization);
            _repository.Save();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrganization(Guid id)
        {
            var _organization = _repository.Organization.GetOrganization(id, trackChanges: false);

            if (_organization == null)
            {
                _logger.LogInfo("Organization with that ID does not exist");
                return NotFound();
            }

            _repository.Organization.deleteOrg(_organization);
            _repository.Save();

            return NoContent();
        }
    }

}