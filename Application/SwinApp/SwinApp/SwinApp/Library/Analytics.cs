namespace SwinApp.Library
{
    public static class Anaytics
    {
        public static void LogAction(string action)
        {

        }

        public static void ConstructLoggedAction()
        {

        }
    }

    public class LoggedAction
    {
        public ActionType ActionType { get; set; }
        public string DateTime { get; set; }
    }
    public enum ActionType
    {
        APP_START = 0,
        CLICK_LINK_ITEM = 1,
        CLICK_MENU_ITEM = 2,


    }
}