namespace algLab_4.Logger
{
    /// <summary> Обработчик Записывающий сообщения в консоль </summary>
    public class ConsoleHandler : IMessageHandler
    {
        /// <summary> Выполнить запись в консоль </summary>
        /// <param name="message"> Сообщение </param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
