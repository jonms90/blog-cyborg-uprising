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
        public static List<Factory> Neutrals;
        public static List<string> Commands;
        public static List<Troop> Enemies;
        private BehaviorTree _behavior;

        /// <summary>
        /// Game initialization parsing input describing the graph that contains the randomly generated battlefield.
        /// </summary>
        public void Initialize()
        {
            _inputParser = new InputParser();
            Battlefield = new Battlefield();
            Factories = new List<Factory>();
            Neutrals = new List<Factory>();
            Targets = new List<Factory>();
            Enemies = new List<Troop>();
            Commands = new List<string>();
            _behavior = new BehaviorTree("Bot");

            var dependency = new BehaviorTree("Available Commands Condition");
            dependency.AddChild(new Leaf("Has Available Cyborgs", HasAvailableCyborgs));
            var loop = new Loop("Issue Troop Commands", dependency);

            var defendFactories = new Leaf("Defend Factories", DefendFactories);

            var increaseProduction = new Selector("Increase Production");
            increaseProduction.AddChild(new IncreaseFactoryProduction("Increase Factory Production"));
            increaseProduction.AddChild(new Expand("Expand To Neutral"));
            var attack = new LaunchAttack("Launch Attack");
            increaseProduction.AddChild(attack);

            var troopSequence = new Sequence("Troop Sequence");
            troopSequence.AddChild(defendFactories);
            troopSequence.AddChild(increaseProduction);

            loop.AddChild(troopSequence);

            var succeeder = new Succeeder("Command Succeeder");
            succeeder.AddChild(loop);

            var strategy = new Sequence("Strategy");
            strategy.AddChild(succeeder);

            var fallbackWait = new Sequence("Fallback Wait");
            var inverter = new Inverter("Commands Inverter");
            var hasCommands = new Leaf("Has issued commands", HasIssuedCommands);
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

        private Node.NodeStatus DefendFactories()
        {
            // Reserve cyborgs to defend the next turn of attacks
            var incomingEnemies = Enemies.Where(e => e.TurnsUntilArrival <= 3).GroupBy(e => e.Destination).ToList();
            foreach (var group in incomingEnemies)
            {
                var targetedFactory = Factories.FirstOrDefault(f => f.Id == group.Key);
                if (targetedFactory == null) // Not an attack
                {
                    continue;
                }

                var attackingCyborgs = group.Sum(g => g.CyborgCount);
                if (targetedFactory.Cyborgs >= attackingCyborgs)
                {
                    targetedFactory.Cyborgs -= attackingCyborgs;
                }
                else
                {
                    targetedFactory.Cyborgs = 0;
                }
            }

            return Node.NodeStatus.Success;
        }

        private Node.NodeStatus HasIssuedCommands()
        {
            return Commands.Count > 0 ? Node.NodeStatus.Success : Node.NodeStatus.Failure;
        }

        public void Update()
        {
            Factories.Clear();
            Targets.Clear();
            Neutrals.Clear();
            Enemies.Clear();
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
                else if(entity.IsNeutralFactory())
                {
                    Neutrals.Add((Factory)entity);
                }
                else if (entity.IsEnemyTroop())
                {
                    Enemies.Add((Troop)entity);
                }
            }

            _behavior.Process();
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
