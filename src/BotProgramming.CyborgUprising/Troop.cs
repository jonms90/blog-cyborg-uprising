namespace BotProgramming.CyborgUprising
{
    public class Troop : Entity
    {
        public int Origin { get; }
        public int Destination { get; }
        public int CyborgCount { get; }
        public int TurnsUntilArrival { get; }

        public Troop(int entityId, Team team, int origin, int destination, int cyborgCount, int turnsUntilArrival) : base(entityId, EntityType.Troop, team)
        {
            Origin = origin;
            Destination = destination;
            CyborgCount = cyborgCount;
            TurnsUntilArrival = turnsUntilArrival;
        }
    }
}
