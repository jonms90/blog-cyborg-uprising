using System;
using System.Collections.Generic;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Bot
    {
        private Battlefield _battlefield;
        private InputParser _inputParser;
        private List<Factory> _factories;
        private List<Factory> _targets;
        private List<string> _commands;

        /// <summary>
        /// Game initialization parsing input describing the graph that contains the randomly generated battlefield.
        /// </summary>
        public void Initialize()
        {
            _inputParser = new InputParser();
            _battlefield = new Battlefield();
            _factories = new List<Factory>();
            _targets = new List<Factory>();
            _commands = new List<string>();
            var factoryCount = _inputParser.ParseNextInteger();
            var linkCount = _inputParser.ParseNextInteger();
            for (var i = 0; i < linkCount; i++)
            {
                var link = _inputParser.ParseNextLink();
                _battlefield.AddFactories(link);
            }
        }

        public void Update()
        {
            _factories.Clear();
            _targets.Clear();
            _commands.Clear();
            var entityCount = _inputParser.ParseNextInteger();
            for (var i = 0; i < entityCount; i++)
            {
                var entity = _inputParser.ParseNextEntity();
                if (entity.IsFriendlyFactory())
                {
                    _factories.Add((Factory)entity);
                }
                else if (entity.IsEnemyFactory())
                {
                    _targets.Add((Factory)entity);
                }
            }

            foreach (var factory in _factories)
            {
                var target = _targets.FirstOrDefault();
                if (target != null)
                {
                    var destination =
                        _battlefield.NextFactoryOnShortestPathBetween(factory, target);
                    _commands.Add($"MOVE {factory.Id} {destination.Id} {factory.Cyborgs}");
                }
            }

            Console.WriteLine(string.Join(';', _commands));
        }
    }
}
