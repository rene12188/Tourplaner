﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tourplaner_Frontend.Commands;

namespace Tourplaner_Frontend
{

    class AddTourViewModel : INotifyPropertyChanged
    {
        public ICommand Submittour
        {
            get;
            set;
        }

    public AddTourViewModel()
        {
            Submittour = new SubmitTour(this);
        }

        private string __tourname;

        public string Tourname
        {

            get
            {
                return __tourname;
            }
            set
            {
                Debug.Write("Tourname Changed");
                __tourname = value;
            }
        }

        private string __source;
        public string Source
        {

            get
            {
                return __source;
            }
            set
            {
                __source = value;
            }
        }

        private string __destination;
        public string Destination
        {

            get
            {
                return __destination;
            }
            set
            {
                __destination = value;
            }
        }

        public bool checkIfEnabeld()
        {
            if (!string.IsNullOrWhiteSpace(__tourname) && !string.IsNullOrWhiteSpace(__source) && !string.IsNullOrWhiteSpace(__destination))
            {
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}