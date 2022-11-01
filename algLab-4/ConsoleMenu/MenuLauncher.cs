namespace algLab_4.ConsoleMenu
{
    /// <summary> Запуск меню </summary>
    public static class MenuLauncher
    {
        /// <summary> Движение по меню </summary>
        /// <param name="menuItems"> Элементы меню </param>
        /// <param name="title"> Заголовок меню </param>
        public static IMenuItem? RunSelectingMenu(this IList<IMenuItem> menuItems, string title = null)
        {
            var exit = false;
            do
            {
                ConsoleHelper.ClearScreen();
                if (title != null)
                {
                    Console.ResetColor();
                    Console.WriteLine(title);
                }
                MenuRenderer.DrawMenu(menuItems);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        menuItems.SelectNextItem();
                        break;
                    case ConsoleKey.UpArrow:
                        menuItems.SelectPrevItem();
                        break;
                    case ConsoleKey.Enter:
                        return menuItems.GetCurrentItem();
                    case ConsoleKey.Escape:
                        exit = false;
                        break;
                }

            } while (!exit);

            return null;
        }

        /// <summary> Выбор следующего элемента меню </summary>
        /// <param name="menuItems"> Элементы меню </param>
        private static void SelectNextItem(this IList<IMenuItem> menuItems)
        {
            for (var i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].IsSelected)
                {
                    menuItems[i].IsSelected = false;
                    if (i >= menuItems.Count - 1) menuItems[0].IsSelected = true;
                    else menuItems[i + 1].IsSelected = true;
                    break;
                }
            }
        }

        /// <summary> Выбор предыдущего элемента меню </summary>
        /// <param name="menuItems"> Элементы меню </param>
        private static void SelectPrevItem(this IList<IMenuItem> menuItems) 
        {
            for (var i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].IsSelected)
                {
                    menuItems[i].IsSelected = false;
                    if (i <= 0) menuItems[^1].IsSelected = true;
                    else menuItems[i - 1].IsSelected = true;
                    break;
                }
            }
        }

        /// <summary> Получить текущий элемент меню </summary>
        /// <param name="menuItems"> Элементы меню </param>
        private static IMenuItem? GetCurrentItem(this IList<IMenuItem> menuItems)
        {
            return menuItems.FirstOrDefault(menuItem => menuItem.IsSelected);
        }
    }
}
