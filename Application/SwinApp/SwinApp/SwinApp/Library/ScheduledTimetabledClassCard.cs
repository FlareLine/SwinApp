using System;

using Xamarin.Forms;

using SwinApp.Components.UI;

namespace SwinApp.Library
{
    class ScheduledTimetabledClassCard : IDashCard
    {

        private Grid _content;
        private TimetabledClass _class;

        public ScheduledTimetabledClassCard(TimetabledClass c)
        {
            _class = c;
            _content = new CardScheduledTimetabledClass(_class);
        }

        public string Title => _class.Type;

        public Grid Content => _content;

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
