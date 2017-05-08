using System;

namespace SwinApp.Library
{
	/// <summary>
	/// A lesson (e.g., Lecture) from one of the units that the
	/// user is enrolled in.
	/// </summary>
    public class Lesson : IPlanned
    {
		// There are probably more types than this...
		// The more I type, the less useful I think this enumeration will be
		enum LessonType
		{
			Lecture,
			Tutorial,
			Lab,
			Workshop
		}

		private string _name;	// Unit name
		private DateTime _time;

		private string _unitCode;
		private string _location;
		private LessonType _type;	// The type of lesson (e.g., Lecture, Tutorial)

		public Lesson(string name, DateTime time, string unitCode, string location, string type) {
			Name = name;
			Time = time;

			UnitCode = unitCode;
			Location = location;
			Type = type;
		}

		public string Name { get => _name; set => _name = value; }
		public DateTime Time { get => _time; set => _time = value; }
		public TimeSpan When => Time - DateTime.Now;

		public string UnitCode { get => _unitCode; set => _unitCode = value; }
		public string Location { get => _location; set => _location = value; }
		public string Type {
			get => Enum.GetName(typeof(LessonType), _type);
			set {
				if (!Enum.TryParse(value, true, out _type)) {
					_type = LessonType.Lecture;
				}
			}
		}		

		/// <summary>
		/// Not yet implemented
		/// </summary>
		public void Refresh() {
			throw new NotImplementedException();
		}
    }
}