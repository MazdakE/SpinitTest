using SpinitTest.Models;
using SpinitTest.Queries;

namespace SpinitTest.Services.Interfaces
{
    public interface IStateService
    {
        Task<StateModel> Get(string year, string id);

        Task<(List<StateModel>, string)> Get(StateQuery query);
    }
}
