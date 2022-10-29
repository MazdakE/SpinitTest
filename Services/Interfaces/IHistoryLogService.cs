using SpinitTest.Models;

namespace SpinitTest.Services.Interfaces
{
    public interface IHistoryLogService
    {
        Task<List<HistoryLogModel>> Get();
    }
}
