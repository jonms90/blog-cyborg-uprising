using System.Collections.Generic;

namespace BotProgramming.CyborgUprising
{
    public class Node
    {
        public enum NodeStatus { Success, Failure }
        public List<Node> Children = new List<Node>();
        public int Priority;

        public Node()
        {
        }

        public Node(int priority)
        {
            Priority = priority;
        }

        public virtual NodeStatus Process()
        {
            return NodeStatus.Success;
        }

        public void AddChild(Node node)
        {
            Children.Add(node);
        }
    }
}
