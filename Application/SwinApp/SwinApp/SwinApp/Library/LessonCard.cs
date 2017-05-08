using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class LessonCard : IDashCard
	{
		private Grid _content;
		private Lesson _lesson;

		public LessonCard(Lesson lesson) {
			_lesson = lesson;
			_content = new CardLesson(_lesson);
		}

		public string Title => _lesson.Type;
		public Grid Content => _content;

		// Not yet implemented
		public void Load() {
			
		}

		// Not yet implemented
		public void Open() {
			
		}
    }
}