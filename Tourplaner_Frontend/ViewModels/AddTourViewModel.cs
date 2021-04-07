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

        private string _tourname;

        public string Tourname
        {

            get
            {
                return _tourname;
            }
            set
            {
                Debug.Write("Tourname Changed");
                _tourname = value;
                OnPropertyChanged(nameof(_tourname));
            }
        }

        private string _source;
        public string Source
        {

            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnPropertyChanged(nameof(_source));
            }
        }

        private string _destination;
        public string Destination
        {

            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
                OnPropertyChanged(nameof(_destination));
            }
        }

        private string _description;

        public string Description
        {

            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(_description));
            }
        }

        public float _distance;

        public float Distance
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
