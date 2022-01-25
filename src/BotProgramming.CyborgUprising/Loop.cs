using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProgramming.CyborgUprising
{
    public class Loop : Node
    {
        public BehaviorTree Dependency { get; }

        public Loop(string name, BehaviorTree dependency) : base(name)
        {
            Dependency = dependency;
        }

        public override NodeStatus Process()
        {
            while (Dependency.Process() != NodeStatus.Failure)
            {
                var childStatus = Children[0].Process();
                if (childStatus == NodeStatus.Failure)
                {
                    return NodeStatus.Failure;
                }
            }
            return NodeStatus.Success;
        }
    }
}
