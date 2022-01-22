namespace BotProgramming.CyborgUprising
{
    public abstract class Entity
    {
        public int Id { get; }
        public EntityType Type { get; }
        public Team Team { get; }

        protected Entity(int id, EntityType type, Team team = Team.Neutral)
        {
            Id = id;
            Team = team;
            Type = type;
        }

        public bool IsFriendlyFactory()
        {
            return Type == EntityType.Factory && Team == Team.Friendly;
        }

        public bool IsEnemyFactory()
        {
            return Type == EntityType.Factory && Team == Team.Enemy;
        }
    }
}
