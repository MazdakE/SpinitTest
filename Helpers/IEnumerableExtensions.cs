using SpinitTest.Services.PropertyMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace SpinitTest.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ApplySort<T>(this IEnumerable<T> source, string orderBy,
               Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByAfterSplit = orderBy.Split(',');
            var orderByStr = "";

            // reverse the order for orderby otherwise it will be ordered the wrong way
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                // trim to remove leading or trailing spaces
                var trimmedOrderByClause = orderByClause.Trim();

                // if the sort option ends with with " desc", we order
                // descending, ortherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // remove " asc" or " desc" from the orderBy clause, so we 
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1
                    ? trimmedOrderByClause
                    : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // find the matching property
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                // get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException(nameof(propertyMappingValue));
                }

                foreach (var destinationProperty in
                    propertyMappingValue.DestinationProperties.Reverse())
                {
                    // revert sort order if necessary
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    orderByStr = orderByStr
                                 + (string.IsNullOrWhiteSpace(orderByStr) ? string.Empty : ", ")
                                 + destinationProperty
                                 + (orderDescending ? " descending" : " ascending");
                }
            }

            return source.AsQueryable().OrderBy(orderByStr);
        }
    }
}