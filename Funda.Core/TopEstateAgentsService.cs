using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Funda.Core
{
    public class TopEstateAgentsService: ITopEstateAgentsService
    {
        private const string UriSeparator = "/";
        private const int IterationsExternalServerLimit = 100;

        public string GetUriString(bool? withGarden)
        {
            var uriString = string.Join(UriSeparator, ConfigurationConstants.BaseUri, ConfigurationConstants.ObjectsJsonPath, 
                ConfigurationConstants.AccessKey, ConfigurationConstants.ForSaleAndSearchRequest, ConfigurationConstants.LocationKey);
            if (withGarden.HasValue && withGarden == true)
                uriString = string.Join(UriSeparator, uriString, ConfigurationConstants.Garden);

            uriString = string.Join(UriSeparator, uriString, ConfigurationConstants.ElementsIteration);
            return uriString;
        }

        public IEnumerable<EstateAgentElement> GetEstateAgentElements(IEnumerable<EstateElement> estateElements)
        {
            var estateElementsGroupedByAgent = estateElements.GroupBy(element => element.EstateAgentId, 
                (estateAgendId, elementValue) => new { EstateAgentId = estateAgendId, ElementValue = elementValue });
            foreach(var agent in estateElementsGroupedByAgent)
            {
                var estateAgentElement = new EstateAgentElement(agent.EstateAgentId, agent.ElementValue.First().EstateAgentName, agent.ElementValue.Count());
                yield return estateAgentElement;
            }
        }

        public IEnumerable<EstateAgentElement> GetTopEstateAgentElements(IEnumerable<EstateAgentElement> estateAgentElements, int numberOfElements)
             => estateAgentElements.Take(numberOfElements);

        public IEnumerable<EstateAgentElement> GetOrderedEstateAgentElements(IEnumerable<EstateAgentElement> estateAgentElements)
            => estateAgentElements.OrderByDescending(element => element.NumberOfEstateObjects);

        public async Task<IEnumerable<EstateElement>> GetEstateElementsAsync(bool? withGarden)
        {
            var uriString = GetUriString(withGarden);

            var estateElements = new HashSet<EstateElement>();
            using (var client = new HttpClient())
            {
                var iteration = 1;

                bool areAnyEstateObjects;
                do
                {
                    var uriStringIteration = string.Format(uriString, iteration.ToString());
                    var response = await client.GetAsync(uriStringIteration).ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode && response.ReasonPhrase.Equals(ConfigurationConstants.RequestLimitExceededError, StringComparison.InvariantCultureIgnoreCase))
                            throw new RequestLimitExceededException($"{ConfigurationConstants.RequestLimitExceededError}. Try again in 1 minute.");

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var responseBodyAsJson = JObject.Parse(responseBody);
                    var responseEstateObjects = responseBodyAsJson.GetValue(ConfigurationConstants.EstateObjects);
                    areAnyEstateObjects = responseEstateObjects.Type == JTokenType.Array && responseEstateObjects.HasValues;

                    foreach (var responseEstateObject in responseEstateObjects)
                    {

                        var estateElementIdAsString = responseEstateObject.Value<string>(ConfigurationConstants.EstateElementId);
                        var estateElementId = new Guid(estateElementIdAsString);
                        var estateAgentId = responseEstateObject.Value<int>(ConfigurationConstants.EstateAgentId);
                        var estateAgentName = responseEstateObject.Value<string>(ConfigurationConstants.EstateAgentName);
                        var estateElement = new EstateElement(estateElementId, estateAgentId, estateAgentName);
                        estateElements.Add(estateElement);
                    }

                    iteration++;
                    //Let's delay our iterations with 60 seconds when wereach limit per minute 
                    if (iteration%IterationsExternalServerLimit==0)
                        await Task.Delay(60000);
                }
                while (areAnyEstateObjects);

            }
            return estateElements;
        }

        public async Task<IEnumerable<EstateAgentElement>> GetTopTenEstateAgentElements(bool? withGarden)
        {
            var estateElements = await GetEstateElementsAsync(withGarden).ConfigureAwait(false);
            var estateAgents = GetEstateAgentElements(estateElements);
            var orderedEstateAgents = GetOrderedEstateAgentElements(estateAgents);
            var topEstateAgents = GetTopEstateAgentElements(orderedEstateAgents, 10);
            return topEstateAgents;
        }
    }
}
