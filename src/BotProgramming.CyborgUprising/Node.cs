using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class Node
    {
        public enum NodeStatus { Success, Running, Failure }

        public NodeStatus Status;
        public List<Node> Children = new List<Node>();
        public int CurrentChild = 0;
        public string Name;
        public int Priority;

        public Node(string name)
        {
            Name = name;
        }

        public Node(string name, int priority)
        {
            Name = name;
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
