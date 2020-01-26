using System;

namespace Funda.Core
{
    public class EstateAgentElement : IEquatable<EstateAgentElement>
    {
        public int EstateAgentId { get; }
        public string EstateAgentName { get; }
        public int NumberOfEstateObjects { get; }

        public EstateAgentElement(int estateAgentId, string estateAgentName, int numberOfEstateObjects)
        {
            EstateAgentId = estateAgentId;
            EstateAgentName = estateAgentName;
            NumberOfEstateObjects = numberOfEstateObjects;
        }

        bool IEquatable<EstateAgentElement>.Equals(EstateAgentElement other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            return obj is EstateAgentElement other
            && other.GetType() == GetType()
            && other.EstateAgentId.Equals(EstateAgentId)
            && other.EstateAgentName.Equals(EstateAgentName)
            && other.NumberOfEstateObjects.Equals(NumberOfEstateObjects);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 41;
                hash = hash * 43 + EstateAgentId.GetHashCode();
                hash = hash * 43 + EstateAgentName.GetHashCode();
                hash = hash * 43 + NumberOfEstateObjects.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return $"# of objects:{NumberOfEstateObjects}, {EstateAgentId} '{EstateAgentName}' ";
        }
    }
}
