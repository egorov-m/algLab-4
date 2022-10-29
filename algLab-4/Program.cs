using algLab_4.Logger;

namespace algLab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var handlers = new List<IMessageHandler>() { new ConsoleHandler(), new FileHandler() };
            var logger = Logger.Logger.GetLogger("logger", Level.INFO, handlers);
            

            for (var i = 0; i < 10; i++)
            {
                logger.Info($"Index: {i}.");
                Thread.Sleep(2000);
            }
        }
    }
}