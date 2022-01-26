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
        public static List<Bomb> EnemyBombs;

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
            EnemyBombs = new List<Bomb>();
            var factoryCount = _inputParser.ParseNextInteger();
            var linkCount = _inputParser.ParseNextInteger();
            for (var i = 0; i < linkCount; i++)
            {
                var link = _inputParser.ParseNextLink();
                Battlefield.AddFactories(link);
            }
        }

        public void Update()
        {
            Factories.Clear();
            Targets.Clear();
            Neutrals.Clear();
            Enemies.Clear();
            Commands.Clear();
            EnemyBombs.ForEach(b => b.SetInactive());
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
                else if (entity.IsNeutralFactory())
                {
                    Neutrals.Add((Factory)entity);
                }
                else if (entity.IsEnemyTroop())
                {
                    Enemies.Add((Troop)entity);
                }
                else if (entity.IsEnemyBomb() && entity is Bomb bomb)
                {
                    var existingBomb = EnemyBombs.FirstOrDefault(b => b.Id == bomb.Id);
                    if (existingBomb != null)
                    {
                        existingBomb.Active = true;
                        existingBomb.IncreaseAge();
                    }
                    else
                    {
                        EnemyBombs.Add(bomb);
                    }
                }
            }

            EnemyBombs.RemoveAll(b => !b.Active);

            foreach (var factory in Factories)
            {
                var factoryBt = new FactoryBehaviorTree(factory);
                factoryBt.Process();
            }

            if (Commands.Count == 0)
            {
                Commands.Add("WAIT");
            }

            Console.WriteLine(string.Join(';', Commands));
        }
    }
}
