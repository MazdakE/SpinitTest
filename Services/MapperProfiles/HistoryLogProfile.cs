using AutoMapper;
using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Queries;

namespace SpinitTest.Services.MapperProfiles
{
    public class HistoryLogProfile : Profile
    {
        public HistoryLogProfile()
        {
            CreateMap<StateQuery, HistoryLogEntity>()
                .ForMember(dest => dest.IdState, opt => opt.MapFrom(src => src.Id));

            CreateMap<HistoryLogEntity, StateModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdState));

            CreateMap<HistoryLogEntity, HistoryLogModel>();
        }
    }
}
