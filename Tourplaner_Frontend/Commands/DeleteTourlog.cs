using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Utility;

namespace Tourplaner_Frontend
{
    
    class DeleteTourlog : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public DeleteTourlog(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;

            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed of Input");
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                
            };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_mainViewModel.SelectedTourlog != null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
           Mainlogic.DeleteTourLog(_mainViewModel.SelectedTourlog);
        }
    }
}
