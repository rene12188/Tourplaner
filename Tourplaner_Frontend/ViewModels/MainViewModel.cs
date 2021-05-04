using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Frontend.Commands;
using Tourplaner_Utility;
using log4net;
using log4net.Config;
using System.Windows.Media.Imaging;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;


namespace Tourplaner_Frontend
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(MainViewModel));

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
        public ICommand Report
        {
            get;
            set;
        }

        public ICommand SetImageFolder
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
                log.Info("Selected Tour = " + value.Name + "\n");
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
                AverageDistance = 0;
                AverageDifficulty = 0;
                AverageRating = 0;
                AverageSpeed = 0;
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
                    log.Info("Selected Tourlog: " + value.TLID);
                    _selectecTourlog = value;
                    OnPropertyChanged(nameof(SelectedTourlog));
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
                log.Info("Searchterm changed Updated");
                SearchTour();
                OnPropertyChanged(nameof(Searchterm));
            }

        }

        public void SearchTour()
        {
            _displaytourlist.Clear();
            /*   foreach (var tour in this._tourlist)
               {
                   if (tour.Name.Contains(_searchterm) || tour.Destination.Contains(_searchterm))
                   {
                       _displaytourlist.Add(tour);
                   }
               }*/
            _displaytourlist = new ObservableCollection<Tour>( _tourlist.Where(x =>
                x.Name.Contains(_searchterm) || x.Description.Contains(_searchterm) ||
                x.Tourlogs.Any(y => y.Report.Contains(_searchterm))));
                
            


          //  _displaytourlist.Add(tmp);
          OnPropertyChanged(nameof(DisplayTourlist));
            log.Info("Displaytourlist changed Selected now contains: "+ _displaytourlist.Count + " Entries");
        }

      

        public void UpdateImage()
        {
            BitmapImage image = null;
            log.Info("Image Updated");
            if (_selectedTour != null)
            {
                if (File.Exists(_selectedTour.Image))
                {
                    log.Info(_selectedTour.Name + "Tour Selected");
                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(_selectedTour.Image);
                    image.EndInit();
                    TourImage = image;
                    return;
                }
            }

            log.Warn("No Tour Selected");
            image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.Default;
            image.UriSource = new Uri(@"./Fallback.JPG", UriKind.Relative);
            image.EndInit();
            TourImage = image;
            
        }

        public void  UpdateTours()
        {
            this._tourlist = Mainlogic.UpdateTours(); 
            this.Searchterm = String.Empty;
            OnPropertyChanged(nameof(Searchterm));
        }
        
        public MainViewModel()
        {
            log.Info("Started Application");
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
            this.Report = new CreateReport(this);
            this.SetImageFolder = new SetImageFolder(this);
        }



        private double _averageDistance;

        public double AverageDistance
        {
            get
            {
                return _averageDistance;
            }
            set
            {
                double sum = 0;
                if (_selectedTour != null)
                {
                    
                    foreach (var TL in _selectedTour.Tourlogs)
                    {
                         sum =+ TL.Distance;
                    }

                    _averageDistance = sum / _selectedTour.Tourlogs.Count;
                    OnPropertyChanged(nameof(AverageDistance));
                }
            
            }
        }

        private double _averageSpeed;

        public double AverageSpeed
        {
            get
            {
                return _averageSpeed;
            }
            set
            {
                double sum = 0;
                if (_selectedTour != null)
                {

                    foreach (var TL in _selectedTour.Tourlogs)
                    {
                        sum = +TL.AvgSpeed;
                    }

                    _averageSpeed = sum / _selectedTour.Tourlogs.Count;
                    OnPropertyChanged(nameof(AverageSpeed));
                }
            }
        }

        private double _averageRating;

        public double AverageRating
        {
            get
            {
                return _averageRating;
            }
            set
            {
                double sum = 0;
                if (_selectedTour != null)
                {

                    foreach (var TL in _selectedTour.Tourlogs)
                    {
                        sum = +TL.Rating;
                    }

                    _averageRating = sum / _selectedTour.Tourlogs.Count;
                    OnPropertyChanged(nameof(_averageRating));
                }
            }
        }

        private double _averageDifficulty;

        public double AverageDifficulty
        {
            get
            {
                return _averageDifficulty;
            }
            set
            {
                double sum = 0;
                if (_selectedTour != null)
                {

                    foreach (var TL in _selectedTour.Tourlogs)
                    {
                        sum = +TL.Difficulty;
                    }

                    _averageDifficulty = sum / _selectedTour.Tourlogs.Count;
                    OnPropertyChanged(nameof(AverageDifficulty));
                }
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
