using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Tourplaner_Utility;

namespace Tourplaner_Frontend
{
    public class MainViewModel : INotifyPropertyChanged
    {
      

        private ObservableCollection<Tour> __tourlist = new ObservableCollection<Tour>();
        public ObservableCollection<Tour> Tourlist
        {
            get
            {
                return __tourlist;
            }
            set
            {

            }
        }




        public string __output = "HI!";
        public string Output
        {
            get
            {
                return "asd" + __input;
            }
        }
        private string __input = "THIS IS INPUT";
        public string Input
        {
            get
            {
                return __input;
            }
            set
            {
                __input = value;
                OnPropertyChanged("Output");
            }
        }

        public ICommand ExecuteCommand
        {
            get;
            set;
        }

        public ICommand AddTour
        {
            get;
            set;
        }

        private Tour __selectedTour = null;

        public Tour SelectedTour
        {
            get
            {
                Debug.Write(__selectedTour.getName());
                return this.__selectedTour;
            }
            set
            {

            }
        }

        public MainViewModel()
        {
            __tourlist.Add(new Tour(1, "Asd", 0.1,0.1,0.1,0.1));
            __selectedTour = __tourlist[0];
            Debug.Print("ctor MainViewModel");
            this.ExecuteCommand = new ExecuteCommand(this);
            this.AddTour = new AddTours(this);

            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
