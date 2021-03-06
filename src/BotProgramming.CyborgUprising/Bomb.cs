namespace BotProgramming.CyborgUprising
{
    public class Bomb : Entity
    {
        public int Origin { get; }
        public int Destination { get; }
        public int Fuse { get; }

        public Bomb(int entityId, Team team, int origin, int destination, int fuse) : base(entityId, EntityType.Bomb, team)
        {
            Origin = origin;
            Destination = destination;
            Fuse = fuse;
        }
    }
}
