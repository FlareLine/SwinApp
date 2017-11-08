using System;

using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class UpNextCard : IDashCard
    {
        private IPlanned _planned;
        private Grid _content;
        private CardUpNext _card;
        public UpNextCard(IPlanned planned)
        {
            _planned = planned;
            _card = new CardUpNext(_planned);
            _content = _card;
        }
        public string Title => "Up Next";

        public Grid Content => _content;

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        //public void UpdateContext(IPlanned planned)
        //{
        //    _planned = planned;
        //    _card.UpdateContext(_planned);
        //}
    }
}
