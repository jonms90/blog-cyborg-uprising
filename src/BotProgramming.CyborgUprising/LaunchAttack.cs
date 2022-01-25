using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class LaunchAttack : Node
    {
        private Factory _source;
        private Factory _target;

        public LaunchAttack(string name) : base(name)
        {
            var attack = new Sequence("LaunchAttack");
            attack.AddChild(new HasFriendlyFactories("Has Friendly Factories"));
            attack.AddChild(new Leaf("Find Factory", FindAvailableCyborgs));
            var targetSelector = new Selector("Found Target");
            var findTarget = new Leaf("Find Target", FindTarget);
            targetSelector.AddChild(findTarget);
            var inverter = new Inverter("Inverter");
            inverter.AddChild(new Leaf("Reserve Cyborgs", ReserveRemainingCyborgs));
            targetSelector.AddChild(inverter);
            attack.AddChild(targetSelector);
            attack.AddChild(new Leaf("Move Troops", ExecuteMoveCommand));
            Children.Add(attack);
        }

        private NodeStatus ReserveRemainingCyborgs()
        {
            _source.Cyborgs = 0;
            return NodeStatus.Success;
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        public Node.NodeStatus ExecuteMoveCommand()
        {
            var nextTarget = Bot.Battlefield.NextFactoryOnShortestPathBetween(_source, _target);
            var moveCount = _source.Cyborgs;
            Bot.Commands.Add($"MOVE {_source.Id} {nextTarget.Id} {moveCount}");
            _source.Cyborgs -= moveCount;
            return Node.NodeStatus.Success;
        }

        public Node.NodeStatus FindAvailableCyborgs()
        {
            var minimumRequiredCyborgs = Bot.Targets.OrderBy(t => t.Cyborgs).FirstOrDefault()?.Cyborgs ?? 0;
            var factory = Bot.Factories.FirstOrDefault(f => f.Cyborgs > minimumRequiredCyborgs);
            if (factory == null)
            {
                return NodeStatus.Failure;
            }

            _source = factory;
            return Node.NodeStatus.Success;
        }

        public Node.NodeStatus FindTarget()
        {
            // Does not care about distance at all for now. Find a weaker target.
            var targets = Bot.Targets.Where(t => t.Cyborgs < _source.Cyborgs).ToList();
            foreach (var t in targets)
            {
                var distance = Bot.Battlefield.Factories[_source.Id].ConnectedFactories.First(f => f.Key.Id == t.Id).Value;
                var extraCyborgsRequired = distance * t.Production;
                if (t.Cyborgs + extraCyborgsRequired > _source.Cyborgs)
                {
                    continue;
                }
                else
                {
                    _target = t;
                    return Node.NodeStatus.Success;
                }
            }

            return Node.NodeStatus.Failure;
        }

    }
}
