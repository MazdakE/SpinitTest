using AutoMapper;
using Newtonsoft.Json;
using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Queries;
using SpinitTest.Repositories.Interfaces;
using SpinitTest.Services.Interfaces;
//using System.Text.Json;

namespace SpinitTest.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateService(IMapper mapper, IStateRepository stateRepository)
        {
            _mapper = mapper;
            _stateRepository = stateRepository;
        }

        public async Task<StateModel> Get(string year, string id)
        {
            var retObj = await _stateRepository.Get(year, id);

            return (retObj is null)
                ? null
                : _mapper.Map<StateEntity, StateModel>(retObj);
        }

        public async Task<(List<StateModel>, string)> Get(StateQuery query)
        {
            var retObj = await _stateRepository.Get(query);

            var pagination =
                new
                {
                    CurrentPage = retObj.CurrentPage,
                    TotalPages = retObj.TotalPages,
                    TotalCount = retObj.TotalCount,
                    PageSize = retObj.PageSize
                };

            var json = JsonConvert.SerializeObject(pagination);

            return (retObj is null)
                ? (null, json)
                : (_mapper.Map<List<StateEntity>, List<StateModel>>(retObj), json);
        }
    }
}
