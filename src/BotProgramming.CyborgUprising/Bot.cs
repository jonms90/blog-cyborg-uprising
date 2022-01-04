using System;

namespace BotProgramming.CyborgUprising
{
    public class Bot
    {
        private Battlefield _battlefield;

        /// <summary>
        /// Game initialization parsing input describing the graph that contains the randomly generated battlefield.
        /// </summary>
        public void Initialize()
        {
            // TODO: Replace default initialization code.
            var factoryCount = int.Parse(Console.ReadLine());
            var linkCount = int.Parse(Console.ReadLine()); // the number of links between factories
            _battlefield = new Battlefield();
            for (var i = 0; i < linkCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                var factoryId = int.Parse(inputs[0]);
                var adjacentFactoryId = int.Parse(inputs[1]);
                var distance = int.Parse(inputs[2]);
                _battlefield.AddFactories(factoryId, adjacentFactoryId, distance);
            }

        }

        public void Update()
        {
            // TODO: Replace default update code.
            var entityCount = int.Parse(Console.ReadLine()); // the number of entities (e.g. factories and troops)
            for (var i = 0; i < entityCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                var entityId = int.Parse(inputs[0]);
                var entityType = inputs[1];
                var arg1 = int.Parse(inputs[2]);
                var arg2 = int.Parse(inputs[3]);
                var arg3 = int.Parse(inputs[4]);
                var arg4 = int.Parse(inputs[5]);
                var arg5 = int.Parse(inputs[6]);
            }

            Console.WriteLine("WAIT"); // Command for doing nothing
        }
    }
}
