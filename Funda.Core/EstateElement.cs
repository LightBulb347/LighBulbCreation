using System;

namespace Funda.Core
{
    public class EstateElement:IEquatable<EstateElement>
    {
        public Guid Id { get; }
        public int EstateAgentId { get; }
        public string EstateAgentName { get; }

        public EstateElement(Guid id, int estateAgentId, string estateAgentName)
        {
            Id = id;
            EstateAgentId = estateAgentId;
            EstateAgentName = estateAgentName;
        }

        bool IEquatable<EstateElement>.Equals(EstateElement other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            return obj is EstateElement other
            && other.GetType() == GetType()
            && other.Id.Equals(Id)
            && other.EstateAgentId.Equals(EstateAgentId)
            && other.EstateAgentName.Equals(EstateAgentName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 41;
                hash = hash * 43 + Id.GetHashCode();
                hash = hash * 43 + EstateAgentId.GetHashCode();
                hash = hash * 43 + EstateAgentName.GetHashCode();

                return hash;
            }
        }
    }
}
