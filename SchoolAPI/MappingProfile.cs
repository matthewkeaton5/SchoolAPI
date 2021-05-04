using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Organization = Entities.DataTransferObjects.Organization;

namespace SchoolAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Models.Organization, Entities.DataTransferObjects.Organization>()
                    .ForMember(c => c.FullAddress,
                        opt => opt.MapFrom(x => string.Join(' ', x.City, x.Country)));

            CreateMap<Entities.DataTransferObjects.OrganizationCreation, Entities.Models.Organization>();
            
            CreateMap<Entities.DataTransferObjects.OrganizationUpdate, Entities.Models.Organization>();
            
            CreateMap<Entities.Models.User, Entities.DataTransferObjects.Users>();

            CreateMap<Entities.DataTransferObjects.UserCreation, Entities.Models.User>();

            CreateMap<Entities.DataTransferObjects.UserUpdate, Entities.Models.User>();
        }

    }
}
