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
    
    class OpenTourWindow : ICommand
    {
        private int count = 0;
        private readonly MainViewModel _mainViewModel;
        TouradderWindow Newwindow = null;

        public OpenTourWindow(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
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
           this.Newwindow = new TouradderWindow();
           Newwindow.Show();
        }
    }
}
