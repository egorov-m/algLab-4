namespace algLab_4.Logger
{
    /// <summary> Интерфейс обработчика логирования </summary>
    public interface IMessageHandler
    {
        /// <summary> Выполнить запись</summary>
        /// <param name="message"> Сообщение </param>
        void Log(string message);
    }
}
