namespace BotProgramming.CyborgUprising
{
    public class Sequence : Node
    {
        public Sequence(string name) : base(name)
        {
        }

        public override NodeStatus Process()
        {
            foreach (var child in Children)
            {
                var childState = child.Process();
                if (childState == NodeStatus.Failure)
                {
                    return NodeStatus.Failure;
                }
            }

            return NodeStatus.Success;
        }
    }
}
