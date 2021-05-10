using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Frontend.Annotations;
using Tourplaner_Utility;

namespace Tourplaner_Frontend.Commands
{
    class CopyTour : ICommand, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel = null;
        public CopyTour(MainViewModel tmp)
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
            if (_mainviewModel.SelectedTour != null)
                return true;
            return false;
        }

        public async void Execute(object? parameter)
        {
            await Mainlogic.CopyTour(_mainviewModel.SelectedTour.Name);
            _mainviewModel.UpdateTours();
        }
        
        public event EventHandler? CanExecuteChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
