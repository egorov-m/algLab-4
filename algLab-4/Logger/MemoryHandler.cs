namespace algLab_4.Logger
{
    /// <summary> Обработчик аккумулирующий сообщения в памяти </summary>
    public class MemoryHandler : IMessageHandler
    {
        /// <summary> Обработчики сообщений </summary>
        private readonly List<IMessageHandler> _handlers = new();
        /// <summary> Буфер для аккумулирования сообщений </summary>
        private readonly List<string> _buffer = new();
        /// <summary> Размер буфера </summary>
        private readonly int _bufferSize;

        /// <summary> Обработчик аккумулирующий сообщения в памяти </summary>
        /// <param name="bufferSize"> Размер буфера </param>
        /// <param name="handlers"> Обработчики сообщений </param>
        public MemoryHandler(int bufferSize, IEnumerable<IMessageHandler> handlers)
        {
            _bufferSize = bufferSize;
            _handlers.AddRange(handlers);
        }

        /// <summary> Логировать сообщение </summary>
        /// <param name="message"> Сообщение </param>
        public void Log(string message)
        {
            _buffer.Add(message);
            if (_buffer.Count >= _bufferSize) LogBuffer();
        }

        /// <summary> Логировать из буфера </summary>
        private void LogBuffer()
        {
            foreach (var handler in _handlers)
            {
                foreach (var item in _buffer)
                {
                    handler.Log(item);
                }
            }
            _buffer.Clear();
        }
    }
}
