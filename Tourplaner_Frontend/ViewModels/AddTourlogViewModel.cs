﻿using System;
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
using Tourplaner_Utility;

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
                OnPropertyChanged(nameof(Time));
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
                OnPropertyChanged(nameof(Report));
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
                try
                {
                    _distance = value;
                    OnPropertyChanged(nameof(_distance));
                }
                catch(Exception e)
                {
                    throw;
                }
               
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
                OnPropertyChanged(nameof(TTime));
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
                OnPropertyChanged(nameof(Temp));
            }
        }

        public ICommand SubmitTourlog
        {
            get;
            set;
        }

        private Tour _selectedtour;
        public Tour SelectedTour
        {
            get
            {
                return _selectedtour;
            }
            set
            {
                this._selectedtour = value;
            }
        }
        public AddTourlogViewModel()
    {
        SelectedTour = MainViewModel.PublicselectedTour;
        this._time = DateTime.Now;
        SubmitTourlog = new SubmitTourlog(this, _selectedtour.Name);
    }

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
