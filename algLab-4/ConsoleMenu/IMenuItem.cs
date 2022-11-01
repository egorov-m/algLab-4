namespace algLab_4.ConsoleMenu
{
    /// <summary> Интерфейс элемента меню </summary>
    public interface IMenuItem
    {
        /// <summary> Идентификатор </summary>
        public MenuItemType Id { get; set; }
        /// <summary> Текст </summary>
        public string Text { get; set; }
        /// <summary> Выбран ли пункт меню </summary>
        public bool IsSelected { get; set; }
    }
}
