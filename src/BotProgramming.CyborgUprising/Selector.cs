namespace BotProgramming.CyborgUprising
{
    public class Selector : Node
    {
        public Selector()
        {
        }

        public override NodeStatus Process()
        {
            foreach (var child in Children)
            {
                var childStatus = child.Process();
                if (childStatus == NodeStatus.Success)
                {
                    return NodeStatus.Success;
                }
            }

            return NodeStatus.Failure;
        }
    }
}
