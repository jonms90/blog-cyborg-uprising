using System;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Expand : Node
    {
        private Factory _target;
        private Factory _source;
        private const int ProductionIncreaseCost = 10;

        public Expand(string name) : base(name)
        {
            Console.Error.WriteLine("Expand constructor");
            var sequence = new Sequence("Expand Sequence");
            sequence.AddChild(new Leaf("Neutral Exists", NeutralFactoriesExists));
            sequence.AddChild(new Leaf("Find Expansion Target", FindExpansionTarget));
            sequence.AddChild(new Leaf("Has Settler", SettlingFactoryExists));
            sequence.AddChild(new Leaf("Execute Move Command", ExecuteMoveCommand));
            Children.Add(sequence);
        }

        public Expand(string name, int priority) : base(name, priority)
        {
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        private Node.NodeStatus ExecuteMoveCommand() // duplicated code. Refactor to not rely on _source and _target
        {
            var nextTarget = Bot.Battlefield.NextFactoryOnShortestPathBetween(_source, _target);
            var moveCount = _source.Cyborgs;
            Bot.Commands.Add($"MOVE {_source.Id} {nextTarget.Id} {moveCount}");
            _source.Cyborgs -= moveCount;
            Bot.Commands.Add($"MSG Expanding to {_target.Id}");
            return Node.NodeStatus.Success;
        }

        private NodeStatus FindExpansionTarget()
        {
            var currentHq = Bot.Factories.FirstOrDefault();
            if (currentHq == null)
            {
                return NodeStatus.Failure;
            }

            var headQuarter = Bot.Battlefield.Factories[currentHq.Id];
            var closestFactories = headQuarter.ConnectedFactories.OrderBy(f => f.Value).Select(f => f.Key);
            var closestExpansionToHq = closestFactories.Where(f => Bot.Neutrals.Any(n => n.Id == f.Id)).Take(2).ToList();
            if (closestExpansionToHq.Count == 0)
            {
                return NodeStatus.Failure;
            }

            if (closestExpansionToHq.Count == 1)
            {
                _target = Bot.Neutrals.First(f => f.Id == closestExpansionToHq.First().Id);
            }
            else
            {
                var candidate1 = Bot.Neutrals.First(f => f.Id == closestExpansionToHq.First().Id);
                var candidate2 = Bot.Neutrals.First(f => f.Id == closestExpansionToHq.Skip(1).First().Id);
                _target = candidate2.Production > candidate1.Production ? candidate2 : candidate1;
            }
            
            return NodeStatus.Success;
        }

        private NodeStatus SettlingFactoryExists()
        {
            var requiredCyborgs = _target.Cyborgs + 1;
            if (_target.Production == 0)
            {
                requiredCyborgs += ProductionIncreaseCost;
            }
            Console.Error.WriteLine($"Required {requiredCyborgs} to expand to {_target.Id}");
            var factory = Bot.Factories.FirstOrDefault(f => f.Cyborgs > requiredCyborgs);
            if (factory == null)
            {
                return NodeStatus.Failure;
            }

            _source = factory;
            return NodeStatus.Success;
        }

        private NodeStatus NeutralFactoriesExists()
        {
            return Bot.Neutrals.Count > 0 ? NodeStatus.Success : NodeStatus.Failure;
        }
    }
}
