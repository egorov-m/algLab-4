namespace algLab_4.ConsoleMenu
{
    /// <summary> Класс элемента меню </summary>
    public class MenuItem
    {
        /// <summary> Идентификатор </summary>
        public MenuItemType Id { get; set; }
        /// <summary> Текст </summary>
        public string Text { get; set; }
        /// <summary> Выбран ли пункт меню </summary>
        public bool IsSelected { get; set; }

        public MenuItem(MenuItemType id, string text, bool isSelected)
        {
            Id = id;
            Text = text;
            IsSelected = isSelected;
        }
    }
}
