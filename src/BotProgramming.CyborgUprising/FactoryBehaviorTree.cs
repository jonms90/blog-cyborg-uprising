namespace BotProgramming.CyborgUprising
{
    public class FactoryBehaviorTree : Node
    {
        private readonly Factory _factory;
        private const int IncCost = 10;
        private const int MaxProduction = 3;

        public FactoryBehaviorTree(Factory factory)
        {
            _factory = factory;
            var actions = new Selector();
            actions.AddChild(new Evacuate(_factory));
            actions.AddChild(new Defend(_factory));
            actions.AddChild(new Expand(_factory));
            actions.AddChild(new Leaf(IncreaseProduction));
            actions.AddChild(new Attack(_factory));

            var repeatSequence = new Sequence();
            repeatSequence.AddChild(new Leaf(HasAvailableCyborgs));
            repeatSequence.AddChild(actions);

            var repeatUntilFail = new RepeatUntilFail();
            repeatUntilFail.AddChild(repeatSequence);
            Children.Add(repeatUntilFail);
        }

        private NodeStatus HasAvailableCyborgs()
        {
            return _factory.Cyborgs > 0 ? NodeStatus.Success : NodeStatus.Failure;
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        private NodeStatus IncreaseProduction()
        {
            if (_factory.Cyborgs < IncCost || _factory.Production == MaxProduction)
            {
                return NodeStatus.Failure;
            }

            Bot.Commands.Add($"INC {_factory.Id}");
            _factory.Cyborgs -= IncCost;
            return NodeStatus.Success;
        }
    }
}
