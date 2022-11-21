using algLab_4.ConsoleMenu;
using algLab_4.Logger;
using algLab_4.Task2;

namespace algLab_4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var sortLogger = Logger.Logger.GetLogger(
                0,
                "sortLogger",
                Level.INFO,
                new List<IMessageHandler>() { new DelayHandler(1000, new List<IMessageHandler>() { new ConsoleHandler(), new FileHandler() }) });
            MenuRenderer.PrimaryMenuRendering();


        }
    }
}