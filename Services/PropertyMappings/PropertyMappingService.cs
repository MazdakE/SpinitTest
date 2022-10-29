using SpinitTest.Entities;
using SpinitTest.Models;

namespace SpinitTest.Services.PropertyMappings;
public class PropertyMappingService : IPropertyMappingService
{
    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<StateModel, StateEntity>(StateMapping()));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        var matchingMapping = _propertyMappings
                        .OfType<PropertyMapping<TSource, TDestination>>();

        var propertyMappings = matchingMapping.ToList();
        if (propertyMappings.Count() == 1)
        {
            return propertyMappings.First().MappingDictionary;
        }

        throw new Exception($"Cannot find property mapping instance for {typeof(TSource)}, {typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
        var propertyMapping = GetPropertyMapping<TSource, TDestination>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        var fieldsAfterSplit = fields.Split(',');

        return (
            from field
                in fieldsAfterSplit
            select field.Trim()
            into trimmedField
            let indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal)
            select indexOfFirstSpace == -1
                ? trimmedField
                : trimmedField.Remove(indexOfFirstSpace))
            .All(propertyName => propertyMapping.ContainsKey(propertyName));
    }

    private static Dictionary<string, PropertyMappingValue> StateMapping()
    {
        return new(StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new PropertyMappingValue(new List<string> { "IdState" }) },
            { "State", new PropertyMappingValue(new List<string> { "State" }) },
            { "IdYear", new PropertyMappingValue(new List<string> { "IdYear" }) },
            { "Year", new PropertyMappingValue(new List<string> { "Year" }) },
            { "Population", new PropertyMappingValue(new List<string> { "Population" }) },
            { "SlugState", new PropertyMappingValue(new List<string> { "SlugState" }) },
        };

    }
}