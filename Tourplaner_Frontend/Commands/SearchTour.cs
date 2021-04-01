using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Input;
using Tourplaner_Utility;

namespace Tourplaner_Frontend
{
    
    class SearchTour : ICommand
    {
        private int count = 0;
        private readonly MainViewModel _mainViewModel;

        public SearchTour(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed");
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                

            };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {

            return true;
        }

        public void Execute(object parameter)
        {
            this._mainViewModel.SearchTour();
        }
    }
}
