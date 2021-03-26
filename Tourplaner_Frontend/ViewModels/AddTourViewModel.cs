using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        ICommand __submittour = null;

        public AddTourViewModel()
        {
            __submittour = new SubmitTour(this);
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

        private double __start_longitude;
        public double Start_longitude
        {

            get
            {
                return __start_longitude;
            }
            set
            {
                __start_longitude = value;
            }
        }

        private double __start_lattitude;
        public double Start_lattitude
        {

            get
            {
                return __start_lattitude;
            }
            set
            {
                __start_lattitude = value;
            }
        }


        private double __finish_longitude;
        public double Finish_longitude
        {

            get
            {
                return __finish_longitude;
            }
            set
            {
                __finish_longitude = value;
            }
        }

        private double __finish_lattitude;

        public double Finish_lattitude
        {

            get
            {
                return __finish_lattitude;
            }
            set
            {
                __finish_lattitude = value;
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
