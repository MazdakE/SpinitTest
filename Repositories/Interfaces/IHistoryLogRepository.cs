using SpinitTest.Entities;
using SpinitTest.Models;
using SpinitTest.Queries;

namespace SpinitTest.Repositories.Interfaces
{
    public interface IHistoryLogRepository
    {
        Task<HistoryLogEntity> Create(StateQuery query);
        Task<List<HistoryLogEntity>> Get();
    }
}
