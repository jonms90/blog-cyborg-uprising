namespace BotProgramming.CyborgUprising
{
    /// <summary>
    /// A pair of connected factories and the distance between them.
    /// </summary>
    public class Link
    {
        public int FactoryId { get; }
        public int AdjacentFactoryId { get; }
        public int Distance { get; }

        public Link(int factoryId, int adjacentFactoryId, int distance)
        {
            FactoryId = factoryId;
            AdjacentFactoryId = adjacentFactoryId;
            Distance = distance;
        }
    }
}
