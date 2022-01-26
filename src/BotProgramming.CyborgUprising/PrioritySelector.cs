using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class PrioritySelector : Node
    {
        public PrioritySelector()
        {
        }

        private void OrderNodes()
        {
            Children = Children.OrderBy(x => x.Priority).ToList();
        }

        public override NodeStatus Process()
        {
            OrderNodes();
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
