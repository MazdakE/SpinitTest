using Newtonsoft.Json;
using SpinitTest.Entities;
using SpinitTest.Helpers;
using SpinitTest.Models;
using SpinitTest.Queries;
using SpinitTest.Repositories.Interfaces;
using SpinitTest.Services.PropertyMappings;
using System.Text;

namespace SpinitTest.Repositories
{
    public class StateRepository : IStateRepository
    {
        protected HttpClient _httpClient = new();
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IHistoryLogRepository _historyLogRepository;

        public StateRepository(IPropertyMappingService propertyMappingService, IHistoryLogRepository historyLogRepository)
        {
            _propertyMappingService = propertyMappingService;
            _historyLogRepository = historyLogRepository;
        }

        public async Task<StateEntity?> Get(string year, string id)
        {
            var item = (await GetDataUsa(year.Trim().Split(',').ToList()))?.SelectMany(x => x.Data).FirstOrDefault(x => x.IdState == id);

            if (item is null)
                return null;

            return item;
        }

        public async Task<PageList<StateEntity>> Get(StateQuery query)
        {
            var years = !string.IsNullOrWhiteSpace(query.Year) 
            ? string.Concat(query.Year.Where(c => !char.IsWhiteSpace(c))).Split(',').ToList()
            : null;

            var dataUsa = await GetDataUsa(years);

            var queryable = await AddFilters(dataUsa?.SelectMany(x => x.Data).ToList(), query);
            return await PageList<StateEntity>.CreateAsync(queryable, query);
        }

        private async Task<List<DataUsa?>> GetDataUsa(List<string> years)
        {
            var data = new List<DataUsa>();

            if (years is null)
            {
                years = new List<string>();
                years.Add("latest");
            }

            foreach (var year in years)
            {
                var httpGet = await _httpClient.GetAsync($"https://datausa.io/api/data?drilldowns=State&measures=Population&year={year}");

                if (!httpGet.IsSuccessStatusCode)
                    return null;

                // Ändra så att alla properties blir ifyllda
                var content = await httpGet.Content.ReadAsStringAsync();

                if (content is null)
                    return null;

                data.Add(JsonConvert.DeserializeObject<DataUsa>(content));
            }

            return data;
        }

        private async Task<IEnumerable<StateEntity>> AddFilters(IEnumerable<StateEntity> queryable, StateQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Id))
            {
                queryable = queryable.Where(x => x?.IdState?.ToUpper() == query.Id.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(query.State))
            {
                queryable = queryable.Where(x => x?.State?.ToUpper() == query.State.ToUpper());
            }

            if (query.IdYear.HasValue)
            {
                queryable = queryable.Where(x => x.IdYear == query.IdYear.Value);
            }

            if (query.Population.HasValue)
            {
                queryable = queryable.Where(x => x.Population > query.Population.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.SlugState))
            {
                queryable = queryable.Where(x => x?.SlugState?.ToUpper() == query.SlugState.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
                queryable = queryable.ApplySort(query.OrderBy, _propertyMappingService.GetPropertyMapping<StateModel, StateEntity>());

            await _historyLogRepository.Create(query);

            return queryable;
        }
    }
}
