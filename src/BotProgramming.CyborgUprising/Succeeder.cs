namespace BotProgramming.CyborgUprising
{
    public class Succeeder : Node
    {
        public Succeeder()
        {
        }

        public override NodeStatus Process()
        {
            Children[0].Process();
            return NodeStatus.Success;
        }
    }
}
