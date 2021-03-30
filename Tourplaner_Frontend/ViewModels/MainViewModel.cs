using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Frontend.Commands;
using Tourplaner_Utility;

namespace Tourplaner_Frontend
{
    public class MainViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {


        private ObservableCollection<Tour> _tourlist = new ObservableCollection<Tour>();
        public ObservableCollection<Tour> Tourlist
        {
            get
            {
                return this._tourlist;
            }
            set
            {
                this._tourlist = value;
            }
        }

        private ObservableCollection<Tour> _displaytourlist = new ObservableCollection<Tour>();
        public ObservableCollection<Tour> DisplayTourlist
        {
            get
            {

                return _displaytourlist;
            }
            set
            {
                _displaytourlist = value;
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

        public ICommand Search
        {
            get;
            set;
        }

        public ICommand Delete
        {
            get;
            set;
        }
        public ICommand Copy
        {
            get;
            set;
        }

        private Tour __selectedTour = null;

        public Tour SelectedTour
        {
            get
            {

                return this.__selectedTour;
            }
            set
            {
                if (value != null)
                {
                    Debug.Write("Selected Tour = " + value.Name + "\n");
                    __selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
                
            }
        }

  
        public string Searchterm { get; set; } = string.Empty;

        public void SearchTour()
        {
            this._tourlist = Mainlogic.UpdateTours();
            this._displaytourlist.Clear();
            foreach (var tour in this._tourlist)
            {
                if (tour.Name.Contains(Searchterm))
                {
                    _displaytourlist.Add(tour);
                }

            }
        }

        public void UpdateSource()
        {
            this._tourlist = Mainlogic.UpdateTours();
            this._displaytourlist = new ObservableCollection<Tour>(_tourlist);
        }

        public MainViewModel()
        {
            this._tourlist = Mainlogic.UpdateTours();
            this._displaytourlist = new ObservableCollection<Tour>(_tourlist);
            this.__selectedTour = _tourlist[0];
            // this.ExecuteCommand = new ExecuteCommand(this);
            this.AddTour = new OpenTourWindow(this);
            this.Search = new SearchTour(this);
            this.Delete = new DeleteTour(this);
            this.Copy = new CopyTour(this);
            // Alternative: https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern#id0090030
            // this.ExecuteCommand = new RelayCommand(() => Output = $"Hello {Input}!");
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
