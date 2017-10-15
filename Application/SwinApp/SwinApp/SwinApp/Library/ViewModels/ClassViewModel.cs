using System;
namespace SwinApp.Library.ViewModels
{
    public class ClassViewModel
    {
        TimetabledClass _class;

        public string TimeOfDay => _class.TimeOfDay;

        public string DateMonth => _class.DateMonth;

        public string Day => _class.Day;

        public string Name => _class.Name;

        public string Room => _class.Room;

        public string HexColor => _class.HexColor;

        public ClassViewModel(TimetabledClass c)
        {
            _class = c;
        }

        public void DeleteTimetabledClass()
        {
            User.DeleteTimetabledClass(_class);
        }
    }
}
