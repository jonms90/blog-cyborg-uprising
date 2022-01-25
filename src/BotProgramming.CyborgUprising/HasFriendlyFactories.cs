namespace BotProgramming.CyborgUprising
{
    public class HasFriendlyFactories : Node
    {
        public HasFriendlyFactories(string name) : base(name)
        {
        }

        public HasFriendlyFactories(string name, int priority) : base(name, priority)
        {
        }

        public override NodeStatus Process()
        {
            return Bot.Factories.Count > 0 ? Node.NodeStatus.Success : Node.NodeStatus.Failure;
        }
    }
}
