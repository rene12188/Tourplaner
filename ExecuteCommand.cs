using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;

namespace TourPlaner
{
    class ExecuteCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }
}
