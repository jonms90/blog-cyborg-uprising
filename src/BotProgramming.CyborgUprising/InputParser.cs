using System;
using System.Runtime.CompilerServices;

namespace BotProgramming.CyborgUprising
{
    public class InputParser
    {
        private const string Factory = "FACTORY";
        private const string Troop = "TROOP";
        private const string Bomb = "BOMB";

        public int ParseNextInteger()
        {
            return int.Parse(Console.ReadLine());
        }

        public Link ParseNextLink()
        {
            var inputs = Console.ReadLine().Split(' ');
            var factoryId = int.Parse(inputs[0]);
            var adjacentFactoryId = int.Parse(inputs[1]);
            var distance = int.Parse(inputs[2]);
            return new Link(factoryId, adjacentFactoryId, distance);
        }

        public Entity ParseNextEntity()
        {
            var inputs = Console.ReadLine().Split(' ');
            var entityId = int.Parse(inputs[0]);
            var entityType = inputs[1];
            var team = Enum.Parse<Team>(inputs[2]);
            var arg2 = int.Parse(inputs[3]);
            var arg3 = int.Parse(inputs[4]);
            var arg4 = int.Parse(inputs[5]);
            var arg5 = int.Parse(inputs[6]);
            Entity entity = null;
            switch (entityType)
            {
                case Factory:
                    entity = new Factory(entityId, team, arg2, arg3, arg4);
                    break;
                case Troop:
                    entity = new Troop(entityId, team, arg2, arg3, arg4, arg5);
                    break;
                case Bomb:
                    entity = new Bomb(entityId, team, arg2, arg3, arg4);
                    break;
                default:
                    throw new SwitchExpressionException($"Unknown entity type: {entityType}");
            }

            return entity;
        }
    }
}
