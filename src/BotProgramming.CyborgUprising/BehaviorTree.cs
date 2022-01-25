using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Stack<Node> stack = new Stack<Node>();
            Node currentNode = this;
            stack.Push(currentNode);

            while (stack.Count != 0)
            {
                Node nextNode = stack.Pop();
                for (int i = nextNode.Children.Count - 1; i >= 0; i++)
                {
                    stack.Push(nextNode.Children[i]);
                }
            }
        }
    }
}
