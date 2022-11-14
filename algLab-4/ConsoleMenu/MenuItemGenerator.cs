namespace algLab_4.ConsoleMenu
{
    /// <summary> Генератор элементов меню </summary>
    public class MenuItemGenerator
    {
        public static List<IMenuItem> GenerateMainMenu()
        {
            return new List<IMenuItem>()
            {
                new MenuItem(MenuItemType.SettingsLogger, "Настройки логгера", true),
                new MenuItem(MenuItemType.Task1, "Задание 1", false),
                new MenuItem(MenuItemType.Task2, "Задание 2", false),
                new MenuItem(MenuItemType.Task3, "Задание 3", false),
                new MenuItem(MenuItemType.Exit, "Выход", false)
            };
        }

        public static List<IMenuItem> GenerateTask1Menu()
        {
            return new List<IMenuItem>()
            {
                new MenuItem(MenuItemType.InsertSort, "Сортировка вставками (Insert Sort)", true),
                new MenuItem(MenuItemType.QuickSort, "Быстрая сортировка Хоара (Quick Sort)", false),
                new MenuItem(MenuItemType.PrimaryMenu, "Вернуться на главное меню", false),
                new MenuItem(MenuItemType.Exit, "Выход", false)
            };
        }

        public static List<IMenuItem> GenerateTask3Menu()
        {
            return new List<IMenuItem>()
            {
                new MenuItem(MenuItemType.BubbleSortText, "Сортировка текста (Bubble Sort) и подсчёт повторений слов", true),
                new MenuItem(MenuItemType.QuickSortText, "Сортировка текста (Quick Sort) и подсчёт повторений слов", false),
                new MenuItem(MenuItemType.RadixSortText, "Сортировка текста (Radix Sort) и подсчёт повторений слов", false),
                new MenuItem(MenuItemType.PrimaryMenu, "Вернуться на главное меню", false),
                new MenuItem(MenuItemType.Exit, "Выход", false)
            };
        }

        public static List<IMenuItem> GenerateSettingsLoggerMenu()
        {
            return new List<IMenuItem>()
            {
                new MenuItem(MenuItemType.SetNameLogger, "Установить имя логгеру", true),
                new MenuItem(MenuItemType.SetDelayLogger, "Установить задержку для логгера", false),
                new MenuItem(MenuItemType.PrimaryMenu, "Вернуться на главное меню", false),
                new MenuItem(MenuItemType.Exit, "Выход", false)
            };
        }
    }
}
