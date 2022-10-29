namespace SpinitTest.Services.PropertyMappings;
public interface IPropertyMappingService
{
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);

    Dictionary<string, PropertyMappingValue> GetPropertyMapping
        <TSource, TDestination>();
}
