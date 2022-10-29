using AutoMapper;
using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Queries;

namespace SpinitTest.Services.MapperProfiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<StateEntity, StateModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdState));
            
            CreateMap<StateModel, StateEntity>()
                .ForMember(dest => dest.IdState, opt => opt.MapFrom(src => src.Id));

            CreateMap<StateQuery, StateEntity>()
                .ForMember(dest => dest.IdState, opt => opt.MapFrom(src => src.Id));
        }
    }
}
