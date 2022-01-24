using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class Inverter : Node
    {
        public Inverter(string name) : base(name)
        {
        }

        public override NodeStatus Process()
        {
            var childStatus = Children[0].Process();
            if (childStatus == NodeStatus.Success)
            {
                return NodeStatus.Failure;
            }

            if (childStatus == NodeStatus.Failure)
            {
                return NodeStatus.Success;
            }

            return childStatus;
        }
    }
}
