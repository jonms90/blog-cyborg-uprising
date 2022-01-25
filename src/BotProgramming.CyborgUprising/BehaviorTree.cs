using System.Collections.Generic;

namespace BotProgramming.CyborgUprising
{
    public class BehaviorTree : Node
    {
        public BehaviorTree(string name) : base(name)
        {
        }

        public override NodeStatus Process()
        {
            return Children[0].Process();
        }

        public void Print()
        {
            var stack = new Stack<Node>();
            Node currentNode = this;
            stack.Push(currentNode);

            while (stack.Count != 0)
            {
                var nextNode = stack.Pop();
                for (var i = nextNode.Children.Count - 1; i >= 0; i++)
                {
                    stack.Push(nextNode.Children[i]);
                }
            }
        }
    }
}
