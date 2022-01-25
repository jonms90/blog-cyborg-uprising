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
            attack.AddChild(new Leaf("Find Target", FindTarget));
            attack.AddChild(new Leaf("Move Troops", ExecuteMoveCommand));
            Children.Add(attack);
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
            var factory = Bot.Factories.FirstOrDefault(f => f.Cyborgs > 0);
            if (factory == null)
            {
                return NodeStatus.Failure;
            }

            _source = factory;
            return Node.NodeStatus.Success;
        }

        public Node.NodeStatus FindTarget()
        {
            if (Bot.Targets.Count == 0)
            {
                return Node.NodeStatus.Failure;
            }

            _target = Bot.Targets.First();
            return Node.NodeStatus.Success;
        }

    }
}
