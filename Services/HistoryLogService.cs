using AutoMapper;
using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Repositories.Interfaces;
using SpinitTest.Services.Interfaces;

namespace SpinitTest.Services
{
    public class HistoryLogService : IHistoryLogService
    {
        private readonly IHistoryLogRepository _historyLogRepository;
        private readonly IMapper _mapper;

        public HistoryLogService(IHistoryLogRepository historyLogRepository, IMapper mapper)
        {
            _historyLogRepository = historyLogRepository;
            _mapper = mapper;
        }

        public async Task<List<HistoryLogModel>> Get()
        {
            var retObj =  await _historyLogRepository.Get();

            return retObj is null 
                ? null
                : _mapper.Map<List<HistoryLogModel>>(retObj);
        }
    }
}
