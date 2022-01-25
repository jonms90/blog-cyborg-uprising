using System;
using System.Collections.Generic;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Battlefield
    {
        public Dictionary<int, Factory> Factories { get; }

        public Battlefield()
        {
            Factories = new Dictionary<int, Factory>();
        }

        /// <summary>
        /// Constructor used primarily for testing.
        /// </summary>
        /// <param name="factories">The existing factories on the battlefield</param>
        public Battlefield(Dictionary<int, Factory> factories)
        {
            Factories = factories;
        }

        public void AddFactories(Link link)
        {
            EnsureCreated(link.FactoryId);
            EnsureCreated(link.AdjacentFactoryId);
            Factories[link.FactoryId].AddBidirectionalNeighbor(Factories[link.AdjacentFactoryId], link.Distance);
        }

        private void EnsureCreated(int factoryId)
        {
            if (!Factories.ContainsKey(factoryId))
            {
                Factories.Add(factoryId, new Factory(factoryId));
            }
        }

        /// <summary>
        ///     Finds the next factory on the shortest path between the source and target factories based on Dijkstra's algorithm.
        /// </summary>
        /// <param name="source">The source factory.</param>
        /// <param name="target">The target factory.</param>
        /// <exception cref="ArgumentException">Thrown if target is the same as source.</exception>
        /// <returns>The next factory on the shortest path between source and target.</returns>
        public Factory NextFactoryOnShortestPathBetween(Factory source, Factory target)
        {
            if (target.Equals(source))
            {
                throw new ArgumentException("Target is equal to source", nameof(target));
            }

            var sourceFactory = Factories[source.Id];
            var destinationFactory = Factories[target.Id];

            var shortestPathsFromSource = new Dictionary<Factory, int>();
            var previousStopoverFactory =
                new Dictionary<Factory, Factory>();

            var visitedFactories = new Dictionary<Factory, bool>();
            var unvisitedFactories = new HashSet<Factory>(Factories.Count);

            shortestPathsFromSource.Add(sourceFactory, 0);
            var currentFactory = sourceFactory;

            while (currentFactory != null)
            {
                visitedFactories.Add(currentFactory, true);
                unvisitedFactories.Remove(currentFactory);

                foreach (var connectedFactory in currentFactory.ConnectedFactories)
                {
                    var adjacentFactory = connectedFactory.Key;
                    if (!visitedFactories.ContainsKey(adjacentFactory))
                    {
                        unvisitedFactories.Add(adjacentFactory);
                    }

                    var distanceToAdjacentFactory = connectedFactory.Value;
                    var distanceThroughCurrentFactory =
                        shortestPathsFromSource[currentFactory] + distanceToAdjacentFactory;

                    if (!shortestPathsFromSource.ContainsKey(adjacentFactory) ||
                        distanceThroughCurrentFactory < shortestPathsFromSource[adjacentFactory])
                    {
                        shortestPathsFromSource[adjacentFactory] = distanceThroughCurrentFactory;
                        previousStopoverFactory[adjacentFactory] = currentFactory;
                    }
                }

                currentFactory = unvisitedFactories.OrderBy(f => shortestPathsFromSource[f])
                    .FirstOrDefault();
            }

            var shortestPath = new List<Factory>();
            var destination = destinationFactory;
            while (destination != sourceFactory)
            {
                shortestPath.Add(destination);
                destination = previousStopoverFactory[destination];
            }

            shortestPath.Reverse();
            return shortestPath.First();
        }
    }
}
