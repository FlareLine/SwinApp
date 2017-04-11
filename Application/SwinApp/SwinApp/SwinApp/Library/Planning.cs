using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// Interface for modelling a planned event like a class or a reminder in SwinApp
    /// </summary>
    public interface IPlanned
    {
        string Name { get; set; }

        DateTime Time { get; set; }
    }
    /// <summary>
    /// Very simple planned event card
    /// </summary>
    public class SamplePlanned : ViewModel, IPlanned
    {
        private string _name;
        private DateTime _time;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    NotifyPropertyChanged("Name");
                    _name = value;
                }
            }
        }
        public DateTime Time
        {
            get => _time;
            set
            {
                if (value != _time)
                {
                    NotifyPropertyChanged("Time");
                    _time = value;
                }
            }
        }

        public SamplePlanned(string name, DateTime time)
        {
            Name = name;
            Time = time;
        }

        public void Load()
        {        }
    }
}
