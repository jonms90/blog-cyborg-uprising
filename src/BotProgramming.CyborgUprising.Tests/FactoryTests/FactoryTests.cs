using Xunit;

namespace BotProgramming.CyborgUprising.Tests.FactoryTests
{
    public class FactoryTests
    {
        [Fact]
        public void ShouldHaveNoConnectedFactoriesAtInitialization()
        {
            var factories = new Factory(1).ConnectedFactories;
            Assert.NotNull(factories);
            Assert.Empty(factories);
        }
    }
}
