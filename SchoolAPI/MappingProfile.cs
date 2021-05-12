using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Organization = Entities.DataTransferObjects.OrganizationDto;

namespace SchoolAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Models.Organization, Entities.DataTransferObjects.OrganizationDto>()
                    .ForMember(c => c.FullAddress,
                        opt => opt.MapFrom(x => string.Join(' ', x.City, x.Country)));

            CreateMap<Entities.DataTransferObjects.OrganizationCreationDto, Entities.Models.Organization>();
            
            CreateMap<Entities.DataTransferObjects.OrganizationUpdateDto, Entities.Models.Organization>();
            
            CreateMap<Entities.Models.User, Entities.DataTransferObjects.UsersDto>();

            CreateMap<Entities.DataTransferObjects.UserCreationDto, Entities.Models.User>();

            CreateMap<Entities.DataTransferObjects.UserUpdateDto, Entities.Models.User>();

            CreateMap<UserRegistrationDto, User>();
        }

    }
}
