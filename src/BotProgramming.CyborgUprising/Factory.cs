using System;
using System.Collections.Generic;

namespace BotProgramming.CyborgUprising
{
    public class Factory : IEqualityComparer<Factory>
    {
        public int Id { get; set; }
        public IDictionary<Factory, int> ConnectedFactories { get; }

        public Factory(int id)
        {
            Id = id;
            ConnectedFactories = new Dictionary<Factory, int>();
        }

        /// <summary>
        /// Adds a factory as an adjacent factory.
        /// Since the game is based on an undirected graph, we add the connection both ways.
        /// </summary>
        /// <param name="adjacentFactory">An adjacent factory</param>
        /// <param name="distance">The distance between factories measured in number of game turns</param>
        public void AddAdjacentFactory(Factory adjacentFactory, int distance)
        {
            if (ConnectedFactories.ContainsKey(adjacentFactory))
            {
                return;
            }

            ConnectedFactories.Add(adjacentFactory, distance);
            adjacentFactory.AddAdjacentFactory(this, distance);
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
