using System;
using System.Collections.Generic;
using Xunit;

namespace BotProgramming.CyborgUprising.Tests.BattlefieldTests
{
    public class NextFactoryOnShortestPathBetweenTests
    {
        [Fact]
        public void ShouldReturnTargetIfClosest()
        {
            var factory0 = new Factory(0);
            var factory1 = new Factory(1);
            var factory2 = new Factory(2);
            factory0.AddBidirectionalNeighbor(factory1, 3);
            factory0.AddBidirectionalNeighbor(factory2, 7);
            factory1.AddBidirectionalNeighbor(factory2, 3);
            var factories = new Dictionary<int, Factory>
            {
                {0, factory0},
                {1, factory1},
                {2, factory2}
            };
            var battlefield = new Battlefield(factories);
            var result = battlefield.NextFactoryOnShortestPathBetween(factory0, factory1);
            Assert.Equal(factory1, result);
        }

        [Fact]
        public void ShouldNotReturnTargetIfNotClosest()
        {
            var factory0 = new Factory(0);
            var factory1 = new Factory(1);
            var factory2 = new Factory(2);
            factory0.AddBidirectionalNeighbor(factory1, 3);
            factory0.AddBidirectionalNeighbor(factory2, 7);
            factory1.AddBidirectionalNeighbor(factory2, 3);
            var factories = new Dictionary<int, Factory>
            {
                {0, factory0},
                {1, factory1},
                {2, factory2}
            };
            var battlefield = new Battlefield(factories);
            var result = battlefield.NextFactoryOnShortestPathBetween(factory0, factory2);
            Assert.NotEqual(factory2, result);
        }

        [Fact]
        public void ShouldReturnNextTargetOnShortestPath()
        {
            var factory0 = new Factory(0);
            var factory1 = new Factory(1);
            var factory2 = new Factory(2);
            var factory3 = new Factory(3);
            factory0.AddBidirectionalNeighbor(factory1, 3);
            factory0.AddBidirectionalNeighbor(factory2, 7);
            factory0.AddBidirectionalNeighbor(factory3, 1);
            factory1.AddBidirectionalNeighbor(factory2, 3);
            factory3.AddBidirectionalNeighbor(factory2, 10);
            var factories = new Dictionary<int, Factory>
            {
                {0, factory0},
                {1, factory1},
                {2, factory2},
                {3, factory3}
            };
            var battlefield = new Battlefield(factories);
            var result = battlefield.NextFactoryOnShortestPathBetween(factory0, factory2);
            Assert.Equal(factory1, result);
        }

        [Fact]
        public void ThrowsArgumentExceptionIfTargetIsEqualToSource()
        {
            var battlefield = new Battlefield();
            var factory = new Factory(0);
            Assert.Throws<ArgumentException>(() => battlefield.NextFactoryOnShortestPathBetween(factory, factory));
        }
    }
}
