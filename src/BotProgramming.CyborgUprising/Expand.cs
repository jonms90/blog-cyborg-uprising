using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Expand : Node
    {
        private readonly Factory _factory;
        private Factory _target;
        private const int ProductionIncreaseCost = 10;

        public Expand(Factory factory)
        {
            _factory = factory;
            var sequence = new Sequence();
            sequence.AddChild(new Leaf(NeutralFactoriesExists));
            sequence.AddChild(new Leaf(FindExpansionTarget));
            sequence.AddChild(new Leaf(HasRequiredCyborgs));
            sequence.AddChild(new Leaf(ExpandToTarget));
            Children.Add(sequence);
        }

        private NodeStatus ExpandToTarget()
        {
            var moveTo = new MoveCyborgs(_factory, _target, CyborgsRequiredToExpand());
            return moveTo.Process();
        }

        public Expand(int priority) : base(priority)
        {
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        private NodeStatus FindExpansionTarget()
        {
            var closestFactories = _factory.ConnectedFactories.OrderBy(f => f.Value).Select(f => f.Key);
            var closestExpansions = closestFactories.Where(f => Bot.Neutrals.Any(n => n.Id == f.Id)).Take(2).ToList();
            if (closestExpansions.Count == 0)
            {
                return NodeStatus.Failure;
            }

            if (closestExpansions.Count == 1)
            {
                _target = Bot.Neutrals.First(f => f.Id == closestExpansions.First().Id);
            }
            else
            {
                var candidate1 = Bot.Neutrals.First(f => f.Id == closestExpansions.First().Id);
                var candidate2 = Bot.Neutrals.First(f => f.Id == closestExpansions.Skip(1).First().Id);
                _target = candidate2.Production > candidate1.Production ? candidate2 : candidate1;
            }

            return NodeStatus.Success;
        }

        private NodeStatus HasRequiredCyborgs()
        {
            var requiredCyborgs = CyborgsRequiredToExpand();
            return _factory.Cyborgs > requiredCyborgs ? NodeStatus.Success : NodeStatus.Failure;
        }

        private int CyborgsRequiredToExpand()
        {
            var requiredCyborgs = _target.Cyborgs + 1;
            if (_target.Production == 0)
            {
                requiredCyborgs += ProductionIncreaseCost;
            }

            return requiredCyborgs;
        }

        private NodeStatus NeutralFactoriesExists()
        {
            return Bot.Neutrals.Count > 0 ? NodeStatus.Success : NodeStatus.Failure;
        }
    }
}
