using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;

namespace Tourplaner_Frontend
{
    public class ExecuteCommand : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public ExecuteCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed");
                if (args.PropertyName == "TName")
                {
                    Debug.Print("command: reveived prop changed of Input");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("command: can execute");
            return !string.IsNullOrWhiteSpace(_mainViewModel.Input);
        }

        public void Execute(object? parameter)
        {
            System.Windows.MessageBox.Show("This is the property: " + _mainViewModel.Input);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
