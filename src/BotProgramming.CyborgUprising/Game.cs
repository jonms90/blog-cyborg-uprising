namespace BotProgramming.CyborgUprising
{
    public class Game
    {
        /// <summary>
        /// Entry point expected by CodinGame.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var bot = new Bot();
            bot.Initialize();
            while (true)
            {
                bot.Update();
            }
        }
    }
}
