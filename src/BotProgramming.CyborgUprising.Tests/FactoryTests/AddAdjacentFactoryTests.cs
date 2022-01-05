using Xunit;

namespace BotProgramming.CyborgUprising.Tests.FactoryTests
{
    public class AddAdjacentFactoryTests
    {
        [Fact]
        public void ShouldAddFactoryToCollection()
        {
            var factory = new Factory(1);
            var adjacent = new Factory(2);
            const int distance = 2;
            factory.AddBidirectionalNeigbor(adjacent, distance);
            var factories = factory.ConnectedFactories;
            Assert.Equal(1, factories.Count);
            Assert.Equal(distance, factories[adjacent]);
        }

        [Fact]
        public void ShouldNotAddAlreadyExistingAdjacentFactoryToCollection()
        {
            var factory = new Factory(1);
            var adjacentFactory = new Factory(2);
            factory.AddBidirectionalNeigbor(adjacentFactory, 1);
            factory.AddBidirectionalNeigbor(adjacentFactory, 1);
            Assert.Equal(1, factory.ConnectedFactories.Count);
        }

        [Fact]
        public void ShouldAddAdjacentFactoryInBothDirections()
        {
            var factory = new Factory(1);
            var adjacent = new Factory(2);
            const int distance = 3;
            factory.AddBidirectionalNeigbor(adjacent, distance);
            Assert.Equal(1, factory.ConnectedFactories.Count);
            Assert.Equal(distance, adjacent.ConnectedFactories[factory]);
        }
    }
}
