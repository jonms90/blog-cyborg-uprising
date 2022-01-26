using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Evacuate : Node
    {
        private readonly Factory _factory;

        public Evacuate(Factory factory)
        {
            _factory = factory;
            var enemyBombPresent = new Leaf(EnemyBombPresent);
            var isBombClose = new Leaf(IsEnemyBombClose);
            var evacuateCyborgs = new MoveCyborgs(_factory, _factory.EvacuationTarget, _factory.Cyborgs);
            var sequence = new Sequence();
            sequence.AddChild(enemyBombPresent);
            sequence.AddChild(isBombClose);
            sequence.AddChild(evacuateCyborgs);
            Children.Add(sequence);
        }

        private NodeStatus IsEnemyBombClose()
        {
            foreach (var bomb in Bot.EnemyBombs)
            {
                if (bomb.Origin == _factory.Id) // We captured the origin.
                {
                    continue;
                }

                var distance = _factory.ConnectedFactories.First(f => f.Key.Id == bomb.Origin)
                    .Value;
                var age = bomb.Age;
                var eta = distance - age;
                if (eta == 1)
                {
                    return NodeStatus.Success;
                }
            }

            return NodeStatus.Failure;
        }

        private NodeStatus EnemyBombPresent()
        {
            return Bot.EnemyBombs.Count > 0 ? NodeStatus.Success : NodeStatus.Failure;
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }
    }
}
