using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        public string Title => "Class";

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
