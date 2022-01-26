using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Defend : Node
    {
        private readonly Factory _factory;

        public Defend(Factory factory)
        {
            _factory = factory;
        }

        public override NodeStatus Process()
        {
            for (int i = 1; i <= 3; i++)
            {
                var turnsUntilArrival = i;
                var incomingCyborgs = Bot.Enemies.Where(e =>
                    e.Destination == _factory.Id && e.TurnsUntilArrival == turnsUntilArrival).Sum(x => x.CyborgCount);
                var adjustedAttackStrength = incomingCyborgs - _factory.Production;
                if (adjustedAttackStrength > 0)
                {
                    _factory.Cyborgs -= adjustedAttackStrength;
                }
            }

            return NodeStatus.Failure;
        }
    }
}
