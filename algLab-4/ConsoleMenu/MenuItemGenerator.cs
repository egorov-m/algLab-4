namespace algLab_4.ConsoleMenu
{
    /// <summary> Генератор элементов меню </summary>
    public class MenuItemGenerator
    {
        public static List<MenuItem> GenerateMainMenu()
        {
            return new List<MenuItem>()
            {
                new(MenuItemType.SettingsLogger, "Настройки логгера", true),
                new(MenuItemType.Task1, "Задание 1", false),
                new(MenuItemType.Task2, "Задание 2", false),
                new(MenuItemType.Task3, "Задание 3", false),
                new(MenuItemType.Exit, "Выход", false)
            };
        }

        public static List<MenuItem> GenerateTask1Menu()
        {
            return new List<MenuItem>()
            {
                new(MenuItemType.InsertSort, "Сортировка вставками (Insert Sort)", true),
                new(MenuItemType.QuickSort, "Быстрая сортировка Хоара (Quick Sort)", false),
                new(MenuItemType.PrimaryMenu, "Вернуться на главное меню", false),
                new(MenuItemType.Exit, "Выход", false)
            };
        }

        public static List<MenuItem> GenerateSettingsLoggerMenu()
        {
            return new List<MenuItem>()
            {
                new(MenuItemType.SetNameLogger, "Установить имя логгеру", true),
                new(MenuItemType.SetDelayLogger, "Установить задержку для логгера", false),
                new(MenuItemType.PrimaryMenu, "Вернуться на главное меню", false),
                new(MenuItemType.Exit, "Выход", false)
            };
        }
    }
}
