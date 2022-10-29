using SpinitTest.Entities;
using SpinitTest.Helpers;
using SpinitTest.Queries;

namespace SpinitTest.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<StateEntity> Get(string year, string id);
        Task<PageList<StateEntity>> Get(StateQuery query);
    }
}
