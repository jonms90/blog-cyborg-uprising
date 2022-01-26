namespace BotProgramming.CyborgUprising
{
    public class Leaf : Node
    {
        public delegate NodeStatus Tick();
        public Tick ProcessMethod;

        public delegate NodeStatus TickMulti(int index);
        public TickMulti ProcessMethodMulti;
        public int Index;

        public Leaf()
        {

        }

        public Leaf(Tick pm)
        {
            ProcessMethod = pm;
        }

        public Leaf(TickMulti pm, int index)
        {
            ProcessMethodMulti = pm;
            Index = index;
        }

        public Leaf(Tick pm, int priority) : base(priority)
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
