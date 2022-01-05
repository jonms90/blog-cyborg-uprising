using System.Collections.Generic;

namespace BotProgramming.CyborgUprising
{
    public class Battlefield
    {
        public Dictionary<int, Factory> Factories { get; }

        public Battlefield()
        {
            Factories = new Dictionary<int, Factory>();
        }

        public void AddFactories(int factoryId, int adjacentFactoryId, int distance)
        {
            EnsureCreated(factoryId);
            EnsureCreated(adjacentFactoryId);
            Factories[factoryId].AddAdjacentFactory(Factories[adjacentFactoryId], distance);
        }

        private void EnsureCreated(int factoryId)
        {
            if (!Factories.ContainsKey(factoryId))
            {
                Factories.Add(factoryId, new Factory(factoryId));
            }
        }
    }
}
