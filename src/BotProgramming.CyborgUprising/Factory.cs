using System;
using System.Collections.Generic;

namespace BotProgramming.CyborgUprising
{
    public class Factory : IEqualityComparer<Factory>
    {
        public int Id { get; set; }
        public IDictionary<Factory, int> ConnectedFactories { get; }

        /// <summary>
        /// Instantiates a factory with a given Id without any known adjacent neighbors.
        /// </summary>
        /// <param name="id">The id of the factory</param>
        public Factory(int id)
        {
            Id = id;
            ConnectedFactories = new Dictionary<Factory, int>();
        }

        /// <summary>
        /// Adds a factory as an adjacent factory.
        /// Since the game is based on an undirected graph, we add the connection both ways.
        /// </summary>
        /// <param name="neighbor">An adjacent factory neighbor</param>
        /// <param name="distance">The distance between factories measured in number of game turns</param>
        public void AddBidirectionalNeighbor(Factory neighbor, int distance)
        {
            if (ConnectedFactories.ContainsKey(neighbor))
            {
                return;
            }

            ConnectedFactories.Add(neighbor, distance);
            neighbor.AddBidirectionalNeighbor(this, distance);
        }

        public bool Equals(Factory x, Factory y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Id == y.Id && Equals(x.ConnectedFactories, y.ConnectedFactories);
        }

        public int GetHashCode(Factory obj)
        {
            return HashCode.Combine(obj.Id, obj.ConnectedFactories);
        }
    }
}
