using System;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Attack : Node
    {
        private readonly Factory _factory;
        private Factory _target;
        private int _distance;
        private int _requiredForces;

        public Attack(Factory factory)
        {
            _factory = factory;
            var sequence = new Sequence();
            var findTarget = new Leaf(FindTarget);
            var evaluateSuccess = new Leaf(EvaluateAttack);
            var attack = new Leaf(AttackTarget);
            sequence.AddChild(findTarget);
            sequence.AddChild(evaluateSuccess);
            sequence.AddChild(attack);
            Children.Add(sequence);
        }

        private NodeStatus AttackTarget()
        {
            return new MoveCyborgs(_factory, _target, _requiredForces).Process();
        }

        private NodeStatus EvaluateAttack()
        {
            var reinforcements = _target.Production * (_distance + 1);
            _requiredForces = _target.Cyborgs + reinforcements + 1;
            Console.Error.WriteLine($"Target {_target.Id} requires {_requiredForces} cyborgs to capture.");
            return _factory.Cyborgs > _requiredForces ? NodeStatus.Success : NodeStatus.Failure;
        }

        private NodeStatus FindTarget()
        {
            if (Bot.Targets.Count == 0)
            {
                return NodeStatus.Failure;
            }

            var target = _factory.ConnectedFactories.OrderBy(x => x.Value)
                .First(x => Bot.Targets.Any(t => t.Id == x.Key.Id));
            _target = Bot.Targets.First(t => t.Id == target.Key.Id);
            _distance = target.Value;
            return NodeStatus.Success;
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }
    }
}
