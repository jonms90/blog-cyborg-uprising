using System;
using System.Collections.Generic;
using System.Linq;

namespace BotProgramming.CyborgUprising
{
    public class Bot
    {
        public static Battlefield Battlefield;
        private InputParser _inputParser;
        public static List<Factory> Factories;
        public static List<Factory> Targets;
        public static List<string> Commands;
        private BehaviorTree _behavior;

        /// <summary>
        /// Game initialization parsing input describing the graph that contains the randomly generated battlefield.
        /// </summary>
        public void Initialize()
        {
            _inputParser = new InputParser();
            Battlefield = new Battlefield();
            Factories = new List<Factory>();
            Targets = new List<Factory>();
            Commands = new List<string>();
            _behavior = new BehaviorTree("Bot");
            
            var dependency = new BehaviorTree("Available Commands Condition");
            dependency.AddChild(new Leaf("Has Available Cyborgs", HasAvailableCyborgs));
            var loop = new Loop("Issue Troop Commands", dependency);
            var attack = new LaunchAttack("Launch Attack");
            loop.AddChild(attack);

            var succeeder = new Succeeder("Command Succeeder");
            succeeder.AddChild(loop);

            var strategy = new Sequence("Strategy");
            strategy.AddChild(succeeder);

            var fallbackWait = new Sequence("Fallback Wait");
            var hasCommands = new Leaf("Has issued commands", HasIssuedCommands);
            var inverter = new Inverter("Commands Inverter");
            inverter.AddChild(hasCommands);
            fallbackWait.AddChild(inverter);
            fallbackWait.AddChild(new Leaf("Wait", ExecuteWaitCommand));
            
            strategy.AddChild(fallbackWait);
            _behavior.AddChild(strategy);
            var factoryCount = _inputParser.ParseNextInteger();
            var linkCount = _inputParser.ParseNextInteger();
            for (var i = 0; i < linkCount; i++)
            {
                var link = _inputParser.ParseNextLink();
                Battlefield.AddFactories(link);
            }
        }

        private Node.NodeStatus HasIssuedCommands()
        {
            return Commands.Count > 0 ? Node.NodeStatus.Success : Node.NodeStatus.Failure;
        }

        public void Update()
        {
            Factories.Clear();
            Targets.Clear();
            Commands.Clear();
            var entityCount = _inputParser.ParseNextInteger();
            for (var i = 0; i < entityCount; i++)
            {
                var entity = _inputParser.ParseNextEntity();
                if (entity.IsFriendlyFactory())
                {
                    Factories.Add((Factory)entity);
                }
                else if (entity.IsEnemyFactory())
                {
                    Targets.Add((Factory)entity);
                }
            }

            _behavior.Process();

            //foreach (var factory in _factories)
            //{
            //    var target = _targets.FirstOrDefault();
            //    if (target != null)
            //    {
            //        var destination =
            //            _battlefield.NextFactoryOnShortestPathBetween(factory, target);
            //        _commands.Add($"MOVE {factory.Id} {destination.Id} {factory.Cyborgs}");
            //    }
            //}

            //if (_commands.Count == 0)
            //{
            //    _commands.Add("WAIT");
            //}
            Console.WriteLine(string.Join(';', Commands));
        }

        public Node.NodeStatus ExecuteWaitCommand()
        {
            Bot.Commands.Add("WAIT");
            return Node.NodeStatus.Success;
        }

        public Node.NodeStatus HasAvailableCyborgs()
        {
            return Factories.Sum(x => x.Cyborgs) > 0 ? Node.NodeStatus.Success : Node.NodeStatus.Failure;
        }
    }
}
