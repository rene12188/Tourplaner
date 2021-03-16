using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Tourplaner_Frontend
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string _output = "HI!";
        public string _input = "THIS IS INPUT";
        public event PropertyChangedEventHandler PropertyChanged;


        public string Output
        {
            get
            {
                return "asd" + _input;
            }
        }

        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                OnPropertyChanged("Output");
            }
        }

        public ICommand Command
        {
            get;
            set;
        }

        public MainViewModel()
        {
            Debug.Print("ctor MainViewModel");
            this.Command = new ExecuteCommand(this);

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");
        }
        public MainViewModel()
        {
            Debug.Print("ctor MainViewModel");
            this.Command = new ExecuteCommand(this);

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
