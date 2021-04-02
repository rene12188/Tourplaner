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
    
    class OpenTourlogWindow : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        Tourlogadderform Newwindow = null;

        public OpenTourlogWindow(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;

            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed");
                if (args.PropertyName == "Input")
                {
                    Debug.Print("command: reveived prop changed of Input");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (Newwindow == null)
            {
                return true;
            }

            return false;
        }

        public void Execute(object parameter)
        {
           Debug.Write("AddTour Window Opened");
           Newwindow = new Tourlogadderform();
           Newwindow.Show();
           Newwindow = null;
        }
    }
}
