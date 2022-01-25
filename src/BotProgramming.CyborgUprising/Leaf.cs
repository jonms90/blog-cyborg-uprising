namespace BotProgramming.CyborgUprising
{
    public class Leaf : Node
    {
        public delegate NodeStatus Tick();
        public Tick ProcessMethod;

        public delegate NodeStatus TickMulti(int index);
        public TickMulti ProcessMethodMulti;
        public int Index;

        public Leaf(string name) : base(name)
        {

        }

        public Leaf(string name, Tick pm) : base(name)
        {
            ProcessMethod = pm;
        }

        public Leaf(string name, TickMulti pm, int index) : base(name)
        {
            ProcessMethodMulti = pm;
            Index = index;
        }

        public Leaf(string name, Tick pm, int priority) : base(name, priority)
        {
            ProcessMethod = pm;
        }

        public override NodeStatus Process()
        {
            if (ProcessMethod != null)
            {
                return ProcessMethod();
            }

            if (ProcessMethodMulti != null)
            {
                return ProcessMethodMulti(Index);
            }

            return NodeStatus.Failure;
        }
    }
}
