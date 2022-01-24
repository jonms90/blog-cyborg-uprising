using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class Selector : Node
    {
        public Selector(string name) : base(name)
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
