using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class LaunchAttack : Node
    {
        private Factory _source;
        private Factory _target;

        public LaunchAttack(string name) : base(name)
        {
            var attack = new Sequence("LaunchAttack");
            attack.AddChild(new Leaf("Has Friendly Factories", HasFriendlyFactories));
            attack.AddChild(new Leaf("Find Factory", FindFriendlyFactory));
            attack.AddChild(new Leaf("Find Target", FindTarget));
            attack.AddChild(new Leaf("Move Troops", ExecuteMoveCommand));
            Children.Add(attack);
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        public Node.NodeStatus HasFriendlyFactories()
        {
            if (Bot.Factories.Count > 0)
            {
                return Node.NodeStatus.Success;
            }

            return Node.NodeStatus.Failure;
        }

        public Node.NodeStatus ExecuteMoveCommand()
        {
            var nextTarget = Bot.Battlefield.NextFactoryOnShortestPathBetween(_source, _target);
            Bot.Commands.Add($"MOVE {_source.Id} {nextTarget.Id} {_source.Cyborgs}");
            return Node.NodeStatus.Success;
        }

        public Node.NodeStatus FindFriendlyFactory()
        {
            _source = Bot.Factories.First();
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
