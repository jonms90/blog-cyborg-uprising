namespace BotProgramming.CyborgUprising
{
    public class MoveCyborgs : Node
    {
        private readonly Factory _source;
        private readonly Factory _target;
        private readonly int _cyborgCount;

        public MoveCyborgs(Factory source, Factory target, int cyborgCount)
        {
            _source = source;
            _target = target;
            _cyborgCount = cyborgCount;
        }

        public override NodeStatus Process()
        {
            var nextTarget = Bot.Battlefield.NextFactoryOnShortestPathBetween(_source, _target);
            Bot.Commands.Add($"MOVE {_source.Id} {nextTarget.Id} {_cyborgCount}");
            _source.Cyborgs -= _cyborgCount;
            return Node.NodeStatus.Success;
        }
    }
}
