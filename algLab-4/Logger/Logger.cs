using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Logger
{
    public class Logger
    {
        /// <summary> Обработчики логирования </summary>
        private readonly List<IMessageHandler> _handlers = new();
        /// <summary> Существующие логгеры </summary>
        private static readonly Dictionary<string, Logger> _loggers = new();

        /// <summary> Имя логгера </summary>
        public string Name { get; private set; }

        /// <summary> Уровень </summary>
        public Level Level { get; set; }

        public Logger(string name, Level level, IMessageHandler handler)
        {
            Initialize(name, level);
            _handlers.Add(handler);
        }

        public Logger(string name, Level level, IEnumerable<IMessageHandler> handlers)
        {
            Initialize(name, level);
            _handlers.AddRange(handlers);
        }

        public Logger(string name)
        {
            Initialize(name, Level.INFO);
            _handlers.Add(new ConsoleHandler());
        }

        public Logger(string name, Level level)
        {
            Initialize(name, level);
            _handlers.Add(new ConsoleHandler());
        }

        /// <summary> Базовая инициализация </summary>
        /// <param name="name"> Имя логгера </param>
        /// <param name="level"> Уровень </param>
        private void Initialize(string name, Level level)
        {
            Name = name;
            Level = level;
            _loggers.Add(name, this);
        }

        /// <summary> Получить логгер по имени </summary>
        /// <param name="name"> Имя логгера </param>
        public static Logger GetLogger(string name) => _loggers.ContainsKey(name) ? _loggers[name] : new Logger(name);

        /// <summary> Получить логгер и задать уровень </summary>
        /// <param name="name"> Имя логгера </param>
        /// <param name="level"> Уровень </param>
        public static Logger GetLogger(string name, Level level)
        {
            if (_loggers.ContainsKey(name))
            {
                _loggers[name].Level = level;
                return _loggers[name];
            }

            return new Logger(name, level);
        }

        /// <summary> Получить логгер, задать уровень и обработчики </summary>
        /// <param name="name"> Имя логгера </param>
        /// <param name="level"> Уровень </param>
        /// <param name="handlers"> Обработчики </param>
        public static Logger GetLogger(string name, Level level, IEnumerable<IMessageHandler> handlers)
        {
            if (_loggers.ContainsKey(name))
            {
                _loggers[name].Level = level;
                _loggers[name]._handlers.AddRange(handlers);
                return _loggers[name];
            }

            return new Logger(name, level, handlers);
        }

        /// <summary> Логировать сообщение </summary>
        /// <param name="level"> Уровень </param>
        /// <param name="message"> Сообщение </param>
        public void Log(Level level, string message) => LogMessage(level, message);

        /// <summary> Логировать сообщение </summary>
        /// <param name="message"> Сообщение </param>
        public void Log(string message) => LogMessage(Level.INFO, message);

        /// <summary> Логировать сообщение установленными обработчиками </summary>
        /// <param name="level"> Уровень </param>
        /// <param name="message"> Сообщение </param>
        private void LogMessage(Level level, string message)
        {
            if (level >= Level)
            {
                var logMessage = $"[{level}] {DateTime.Now:yyyy.MM.dd hh:mm:ss} {Name} {message}";
                foreach (var handler in _handlers)
                {
                    handler.Log(logMessage);
                }
            }
        }

        /// <summary> Логирование — уровень дебаг </summary>
        /// <param name="message"> Сообщение для записи </param>
        public void Debug(string message) => Log(Level.DEBUG, message);

        /// <summary> Логирование — уровень информационного сообщения </summary>
        /// <param name="message"> Сообщение для записи </param>
        public void Info(string message) => Log(Level.INFO, message);

        /// <summary> Логирование — уровень предупреждение </summary>
        /// <param name="message"> Сообщение для записи </param>
        public void Warning(string message) => Log(Level.WARNING, message);

        /// <summary> Логирование — уровень ошибка </summary>
        /// <param name="message"> Сообщение для записи </param>
        public void Error(string message) => Log(Level.ERROR, message);
    }
}
