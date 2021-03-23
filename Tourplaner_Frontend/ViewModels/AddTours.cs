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
    
    class AddTours : ICommand
    {
        private int count = 0;
        private readonly MainViewModel _mainViewModel;

        public AddTours(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mainViewModel.Tourlist.Add(new Tour(2,"asd" + count.ToString(),2,2,2,2));
            count++;
            Debug.Print("Help");
        }
    }
}
