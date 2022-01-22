using System;

namespace BotProgramming.CyborgUprising
{
    public class Bot
    {
        private Battlefield _battlefield;
        private InputParser _inputParser;

        /// <summary>
        /// Game initialization parsing input describing the graph that contains the randomly generated battlefield.
        /// </summary>
        public void Initialize()
        {
            _inputParser = new InputParser();
            var factoryCount = _inputParser.ParseNextInteger();
            var linkCount = _inputParser.ParseNextInteger(); // the number of links between factories
            _battlefield = new Battlefield();
            for (var i = 0; i < linkCount; i++)
            {
                var link = _inputParser.ParseNextLink();
                _battlefield.AddFactories(link);
            }
        }

        public void Update()
        {
            var entityCount = _inputParser.ParseNextInteger(); // the number of entities (e.g. factories and troops)
            for (var i = 0; i < entityCount; i++)
            {
                var entity = _inputParser.ParseNextEntity();
            }

            Console.WriteLine("WAIT"); // Command for doing nothing
        }
    }
}
