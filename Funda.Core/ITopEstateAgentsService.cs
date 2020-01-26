using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funda.Core
{
    public interface ITopEstateAgentsService
    {
        string GetUriString(bool? withGarden);
        IEnumerable<EstateAgentElement> GetEstateAgentElements(IEnumerable<EstateElement> estateElements);
        IEnumerable<EstateAgentElement> GetTopEstateAgentElements(IEnumerable<EstateAgentElement> estateAgentElements, int numberOfElements);
        IEnumerable<EstateAgentElement> GetOrderedEstateAgentElements(IEnumerable<EstateAgentElement> estateAgentElements);
        Task<IEnumerable<EstateElement>> GetEstateElementsAsync(bool? withGarden);
        Task<IEnumerable<EstateAgentElement>> GetTopTenEstateAgentElements(bool? withGarden);

    }
}
