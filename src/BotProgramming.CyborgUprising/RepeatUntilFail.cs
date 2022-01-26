namespace BotProgramming.CyborgUprising
{
    public class RepeatUntilFail : Node
    {
        public override NodeStatus Process()
        {
            var childStatus = NodeStatus.Success;
            while (childStatus != NodeStatus.Failure)
            {
                childStatus = Children[0].Process();
            }

            return NodeStatus.Success;
        }
    }
}
