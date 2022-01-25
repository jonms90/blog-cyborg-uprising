using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class IncreaseFactoryProduction : Node
    {
        private Factory _source;
        private const int IncCost = 10;
        private const int MaxProduction = 3;

        public IncreaseFactoryProduction(string name) : base(name)
        {
            var increase = new Sequence("IncreaseProduction");
            increase.AddChild(new HasFriendlyFactories("Has Friendly Factories"));
            increase.AddChild(new Leaf("Find Factory", FindEligibleFactory));
            increase.AddChild(new Leaf("Increase", ExecuteIncreaseCommand));
            Children.Add(increase);
        }

        private NodeStatus ExecuteIncreaseCommand()
        {
            if (_source.Cyborgs < IncCost)
            {
                return NodeStatus.Failure;
            }

            Bot.Commands.Add($"INC {_source.Id}");
            _source.Cyborgs -= IncCost;
            return NodeStatus.Success;
        }

        private NodeStatus FindEligibleFactory()
        {
            var factory = Bot.Factories.FirstOrDefault(f =>
                f.Cyborgs >= IncCost && f.Production < MaxProduction);
            if (factory == null)
            {
                return NodeStatus.Failure;
            }

            _source = factory;
            return NodeStatus.Success;
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        public IncreaseFactoryProduction(string name, int priority) : base(name, priority)
        {
        }
    }
}
