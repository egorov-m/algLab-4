using algLab_4.Logger;

namespace algLab_4.ConsoleMenu
{
    /// <summary> Рендерер консольного меню </summary>
    public class MenuRenderer
    {
        /// <summary> Нарисовать меню </summary>
        /// <param name="menuItems"> Элемент меню </param>
        public static void DrawMenu(IList<IMenuItem> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                var currentOption = menuItem.Text;
                if (menuItem.IsSelected)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"<< {currentOption} >>");
            }

            Console.ResetColor();
        }

        /// <summary> Рендеринг первичного меню </summary>
        public static void PrimaryMenuRendering()
        {
            Console.CursorVisible = false;
            var title = 
@"  _           _       _  _        _____            _           _                  _ _   _                   
 | |         | |     | || |  _   / ____|          | |         | |                (_) | | |                  
 | |     __ _| |__   | || |_(_) | (___   ___  _ __| |_    __ _| | __ _  ___  _ __ _| |_| |__  _ __ ___  ___ 
 | |    / _` | '_ \  |__   _|    \___ \ / _ \| '__| __|  / _` | |/ _` |/ _ \| '__| | __| '_ \| '_ ` _ \/ __|
 | |___| (_| | |_) |    | |  _   ____) | (_) | |  | |_  | (_| | | (_| | (_) | |  | | |_| | | | | | | | \__ \
 |______\__,_|_.__/     |_| (_) |_____/ \___/|_|   \__|  \__,_|_|\__, |\___/|_|  |_|\__|_| |_|_| |_| |_|___/
                                                                  __/ |                                     
                                                                 |___/                                      ";
            var selectItem = MenuItemGenerator.GenerateMainMenu().RunSelectingMenu(title);

            if (selectItem != null)
            {
                switch (selectItem.Id)
                {
                    case MenuItemType.SettingsLogger:
                        SettingsLoggerRendering();
                        break;
                    case MenuItemType.Task1:
                        Task1MenuRendering();
                        break;
                    case MenuItemType.Task2:
                        
                        break;
                    case MenuItemType.Task3:
                        Task3MenuRendering();
                        break;
                    case MenuItemType.Exit:
                        break;
                }
            }
        }

        /// <summary> Рендеринг меню настроек логгера </summary>
        public static void SettingsLoggerRendering()
        {
            Console.CursorVisible = false;
            var title = 
@"   _____      _   _   _                   _                                 
  / ____|    | | | | (_)                 | |                                
 | (___   ___| |_| |_ _ _ __   __ _ ___  | |     ___   __ _  __ _  ___ _ __ 
  \___ \ / _ \ __| __| | '_ \ / _` / __| | |    / _ \ / _` |/ _` |/ _ \ '__|
  ____) |  __/ |_| |_| | | | | (_| \__ \ | |___| (_) | (_| | (_| |  __/ |   
 |_____/ \___|\__|\__|_|_| |_|\__, |___/ |______\___/ \__, |\__, |\___|_|   
                               __/ |                   __/ | __/ |          
                              |___/                   |___/ |___/           
";
            var selectItem = MenuItemGenerator.GenerateSettingsLoggerMenu().RunSelectingMenu(title);
            if (selectItem != null)
            {
                switch (selectItem.Id)
                {
                    case MenuItemType.SetNameLogger:
                        Console.CursorVisible = true;
                        Console.Write("Введите имя логгера: ");
                        var name = Console.ReadLine();
                        if (name is null or "")
                        {
                            Console.WriteLine("\nИмя не может быть пустым, настройки логгера не изменились.");
                        }
                        else
                        {
                            Task1.Extensions.SetLogger(name);
                        }
                        Executor.ExecuteReturn(SettingsLoggerRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.SetDelayLogger:
                        Console.CursorVisible = true;
                        Console.Write("Введите задержку логирования в миллисекундах: ");
                        var str = Console.ReadLine();
                        if (!int.TryParse(str, out var delay)) Console.WriteLine("\nНужно ввести одно целое число миллисекунд, настройки логгера не изменились.");
                        else
                        {
                            Task1.Extensions.GetCurrentLogger().ClearHandlers();
                            Task1.Extensions.GetCurrentLogger()
                                .AddHandler(new DelayHandler(delay, new List<IMessageHandler>() { new ConsoleHandler(), new FileHandler()}));
                        }
                        Executor.ExecuteReturn(SettingsLoggerRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.PrimaryMenu:
                        PrimaryMenuRendering();
                        break;
                    case MenuItemType.Exit:
                        break;
                }
            }
        }

        /// <summary> Рендеринг меню первого задания </summary>
        public static void Task1MenuRendering()
        {
            Console.CursorVisible = false;
            var title = 
@"  _______        _      __ 
 |__   __|      | |    /_ |
    | | __ _ ___| | __  | |
    | |/ _` / __| |/ /  | |
    | | (_| \__ \   <   | |
    |_|\__,_|___/_|\_\  |_|
";
            var selectItem = MenuItemGenerator.GenerateTask1Menu().RunSelectingMenu(title);

            if (selectItem != null)
            {
                switch (selectItem.Id)
                {
                    case MenuItemType.InsertSort:
                        Executor.ExecuteInsertionSort();
                        Executor.ExecuteReturn(Task1MenuRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.QuickSort:
                        Executor.ExecuteQuickSort();
                        Executor.ExecuteReturn(Task1MenuRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.PrimaryMenu:
                        PrimaryMenuRendering();
                        break;
                    case MenuItemType.Exit:
                        break;
                }
            }
        }

        /// <summary> Рендеринг меню третьего задания </summary>
        public static void Task3MenuRendering()
        {
            Console.CursorVisible = false;
            var title = 
@"  _______        _      ____  
 |__   __|      | |    |___ \ 
    | | __ _ ___| | __   __) |
    | |/ _` / __| |/ /  |__ < 
    | | (_| \__ \   <   ___) |
    |_|\__,_|___/_|\_\ |____/ 
";
            var selectItem = MenuItemGenerator.GenerateTask3Menu().RunSelectingMenu(title);

            if (selectItem != null)
            {
                switch (selectItem.Id)
                {
                    case MenuItemType.BubbleSortText:
                        Executor.ExecuteBubbleSortText();
                        Executor.ExecuteReturn(Task3MenuRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.QuickSortText:
                        Executor.ExecuteQuickSortText();
                        Executor.ExecuteReturn(Task3MenuRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.BurstSortText:
                        Executor.ExecuteBurstSortText();
                        Executor.ExecuteReturn(Task3MenuRendering, PrimaryMenuRendering);
                        break;
                    case MenuItemType.PrimaryMenu:
                        PrimaryMenuRendering();
                        break;
                    case MenuItemType.Exit:
                        break;
                }
            }
        }
    }
}
