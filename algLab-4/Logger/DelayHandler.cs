namespace algLab_4.Logger
{
    /// <summary> Обработчик логирования с задержкой </summary>
    public class DelayHandler : IMessageHandler
    {
        /// <summary> Обработчики сообщений </summary>
        private readonly List<IMessageHandler> _handlers = new();
        /// <summary> Задержка логирования в миллисекундах </summary>
        private int _delay;

        /// <summary> Установить задержку логирования </summary>
        /// <param name="delay"> Задержка в миллисекундах </param>
        public void SetDelay(int delay) => _delay = delay;

        public DelayHandler(int delay, IEnumerable<IMessageHandler> handlers)
        {
            _delay = delay;
            _handlers.AddRange(handlers);
        }

        public DelayHandler(IEnumerable<IMessageHandler> handlers)
        {
            _delay = 1000;
            _handlers.AddRange(handlers);
        }

        public DelayHandler()
        {
            _delay = 1000;
            _handlers.Add(new ConsoleHandler());
        }

        /// <summary> Выполнить запись сообщения с задержкой указанными обработчиками </summary>
        /// <param name="message"> Сообщение </param>
        public void Log(string message)
        {
            var t = Task.Run(async () =>
            {
                await Task.Delay(_delay);
                foreach (var handler in _handlers)
                {
                    handler.Log(message);
                }
            });
            t.Wait();
        }
    }
}
