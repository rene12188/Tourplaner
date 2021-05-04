using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Utility;

namespace Tourplaner_Frontend.Commands
{
    class DeleteTour : ICommand, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel = null;
        public DeleteTour(MainViewModel tmp)
        {
            this._mainviewModel = tmp;
            _mainviewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed");
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            };

        }

        public bool CanExecute(object? parameter)
        {
            if(_mainviewModel.SelectedTour != null)    
              return true;
            return false;
        }

        public async void  Execute(object? parameter)
        {
          
            try
            {
                Tour tmp = _mainviewModel.SelectedTour;

                _mainviewModel.SelectedTour = null;
                _mainviewModel.UpdateImage();
                _mainviewModel.TourImage = null;
                int i = await Mainlogic.DeleteTour(tmp);
                _mainviewModel.UpdateImage();
                _mainviewModel.UpdateTours();


            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler? CanExecuteChanged;
    }
}
