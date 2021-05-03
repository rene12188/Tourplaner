using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplaner_Frontend.Commands;
using System.Windows.Controls;

namespace Tourplaner_Frontend
{

    class AddTourlogViewModel : INotifyPropertyChanged
    {
        private DateTime _time;
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        private int _rating;
        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value + 1;
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value+1;
            }
        }


        private string _report;
        public string Report
        {
            get
            {
                return _report;
            }
            set
            {
                _report = value;
            }
        }

        private double _distance;
        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
            }
        }
        private int _ttime;

        public int TTime
        {
            get
            {
                return _ttime;
            }
            set
            {
                _ttime = value;
            }
        }
        private int _temp;
        public int Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
            }
        }

        public ICommand SubmitTourlog
        {
            get;
            set;
        }

        private string _selectedtourname;
        public string SelectedTourName
        {
            get
            {
                return _selectedtourname;
            }
            set
            {
                this._selectedtourname = value;
            }
        }
        public AddTourlogViewModel(MainViewModel mainViewModel)
    {
        this._time = DateTime.Now;
        SubmitTourlog = new SubmitTourlog(this, _selectedtourname);
    }

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
