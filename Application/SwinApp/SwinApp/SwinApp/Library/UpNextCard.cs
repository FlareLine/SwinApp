using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class UpNextCard : IDashCard
    {
        private IPlanned _planned;
        private Grid _content;
        public UpNextCard(IPlanned planned)
        {
            _planned = planned;
            _content = new CardUpNext(_planned);
        }
        public string Title => "Up Next";

        public Grid Content => _content;

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}
