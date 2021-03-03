using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace TourPlaner
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _output = "HI!";
        private string _input = "THIS IS INPUT";
        public event PropertyChangedEventHandler PropertyChanged;


        public string Input
        {
            get
            {
                return _input;
            }
        }
    }
}
