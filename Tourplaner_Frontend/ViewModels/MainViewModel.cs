using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Frontend.Commands;
using Tourplaner_Utility;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;

namespace Tourplaner_Frontend
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BitmapImage _tourImage = null;

        public BitmapImage TourImage
        {
            get
            {
                return _tourImage;
            }
            set
            {
                _tourImage = value;
            }
        }

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

        public ICommand AddLog
        {
            get;
            set;
        }

        public ICommand DeleteLog
        {
            get;
            set;
        }
        public ICommand Export
        {
            get;
            set;
        }

        public ICommand Import
        {
            get;
            set;
        }

        public static Tour _publicselectedTour = null;
        public static Tour PublicselectedTour
        {
            get
            {

                return _publicselectedTour;
            }
            set
            {
                Debug.Write("Selected Tour = " + value.Name + "\n");
                _publicselectedTour = value;
            }
        }

        private Tour _selectedTour = null;
        public Tour SelectedTour
        {
            get
            {

                return _selectedTour;
            }
            set
            {
                _selectedTour = value;
                _publicselectedTour = value;
                UpdateImage();
                OnPropertyChanged(nameof(TourImage));
                OnPropertyChanged(nameof(SelectedTour));



            }
        }

        private Tourlog _selectecTourlog = null;

        public Tourlog SelectedTourlog
        {
            get
            {
                return _selectecTourlog;
            }
            set
            {
                if(value != null)
                {
                    Debug.WriteLine("Selected Tourlog: " + value.TLID);
                    _selectecTourlog = value;
                    OnPropertyChanged(nameof(_selectecTourlog));
                }

            }
        }

        private string _searchterm = string.Empty;

        public string Searchterm {
            get
            {
                return _searchterm;
            }
            set
            {
                _searchterm = value;
                SearchTour();
                OnPropertyChanged(nameof(_searchterm));
            }

        }

        public void SearchTour()
        {
            this._tourlist = Mainlogic.UpdateTours();
            this._displaytourlist.Clear();
               foreach (var tour in this._tourlist)
            {
                if (tour.Name.Contains(_searchterm) || tour.Destination.Contains(_searchterm))
                {
                    _displaytourlist.Add(tour);
                }
            }
            OnPropertyChanged(nameof(_searchterm));
        }

        public void UpdateImage()
        {
            if (_selectedTour != null)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_selectedTour.Image);
                image.EndInit();
                TourImage = image;
            }
            else
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.Default;
                image.UriSource = new Uri(@"./Fallback.JPG", UriKind.Relative);
                image.EndInit();
                TourImage = image;
            }
        }

        public void UpdateTours()
        {
            this._tourlist = Mainlogic.UpdateTours();
        }
        
        public MainViewModel()
        {
            UpdateImage();
            this._tourlist = Mainlogic.UpdateTours();
            this._displaytourlist = new ObservableCollection<Tour>(_tourlist);
            this._selectedTour = null;
            this.AddTour = new OpenTourWindow(this);
            this.Search = new SearchTour(this);
            this.Delete = new DeleteTour(this);
            this.Copy = new CopyTour(this);
            this.AddLog = new OpenTourlogWindow(this);
            this.DeleteLog = new DeleteTourlog(this);
            this.Export = new ExportTours(this);
            this.Import = new ImportTour(this);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
