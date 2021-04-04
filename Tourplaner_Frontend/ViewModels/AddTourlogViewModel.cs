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
      
        public enum _difficulty
        {
            VeryEasy = 1,
            Easy = 2,
            Medium = 3,
            Hard = 4,
            VeryHard = 5,
        }

        public ICommand Submittourlog
        {
            get;
            set;
        }

        private string _report;
        public string Report { get; set; }

        private int _distance;
        public int Distance { get; set; }
        private int _ttime;
        public int TTime { get; set; }
        private int _temp;
        public int Temp { get; set; }

    public AddTourlogViewModel()
        {
            Submittourlog = new SubmitTourlog(this);
        }

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
