using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardLesson : Grid
	{
		public Lesson _lesson;

		public CardLesson(Lesson lesson) {
			_lesson = lesson;
			BindingContext = _lesson;
			InitializeComponent();
		}
	}
}