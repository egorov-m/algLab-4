namespace algLab_4.Logger
{
    /// <summary> Обработчик записывающий сообщения в файл </summary>
    public class FileHandler : IMessageHandler
    {
        /// <summary> Имя файла </summary>
        private string _fileName;

        public FileHandler() => _fileName = "log";

        public FileHandler(string fileName) => _fileName = fileName;

        /// <summary> Установаить имя файла лога </summary>
        /// <param name="fileName"> Имя файла </param>
        public void SetFileName(string fileName) => _fileName = fileName;

        /// <summary> Выполнить запись сообщения в файл </summary>
        /// <param name="message"> Сообщение </param>
        public async void Log(string message)
        {
            await using var writer = new StreamWriter($"{_fileName}.txt", append: true);
            writer.AutoFlush = true;
            await writer.WriteLineAsync(message);
        }
    }
}
