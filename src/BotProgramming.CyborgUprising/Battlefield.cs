using System.Collections.Generic;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Battlefield
    {
        public HashSet<Factory> Factories { get; }
        private readonly HashSet<int> _knownFactories;

        public Battlefield()
        {
            Factories = new HashSet<Factory>();
            _knownFactories = new HashSet<int>();
        }

        public void AddFactories(int factoryId, int adjacentFactoryId, int distance)
        {
            Factory factory = null;
            Factory adjacentFactory = null;
            if (!_knownFactories.Contains(factoryId))
            {
                factory = new Factory(factoryId);
                Factories.Add(factory);
                _knownFactories.Add(factoryId);
            }

            if (!_knownFactories.Contains(adjacentFactoryId))
            {
                adjacentFactory = new Factory(adjacentFactoryId);
                Factories.Add(adjacentFactory);
                _knownFactories.Add(adjacentFactoryId);
            }

            factory ??= Factories.First(f => f.Id == factoryId);
            adjacentFactory ??= Factories.First(f => f.Id == adjacentFactoryId);
            factory.AddAdjacentFactory(adjacentFactory, distance);
        }
    }
}
