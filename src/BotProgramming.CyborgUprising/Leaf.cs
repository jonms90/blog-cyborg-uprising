using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class Leaf : Node
    {
        public delegate NodeStatus Tick();
        public Tick ProcessMethod;

        public Leaf(string name) : base(name)
        {

        }

        public Leaf(string name, Tick pm) : base(name)
        {
            ProcessMethod = pm;
        }

        public override NodeStatus Process()
        {
            if (ProcessMethod != null)
            {
                return ProcessMethod();
            }

            return NodeStatus.Failure;
        }
    }
}
