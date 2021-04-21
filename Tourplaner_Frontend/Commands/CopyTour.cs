using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
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
    class CopyTour : ICommand
    {
        private readonly MainViewModel __mainviewmodel = null;
        public CopyTour(MainViewModel tmp)
        {
            this.__mainviewmodel = tmp;

            __mainviewmodel.PropertyChanged += (sender, args) =>
            {

                Debug.Print("command: reveived prop changed of Input");
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            };
        }

        public bool CanExecute(object? parameter)
        {
        /*    if (!string.IsNullOrWhiteSpace(__addtourviewmodel.Tourname) && !string.IsNullOrWhiteSpace(__addtourviewmodel.Source) && !string.IsNullOrWhiteSpace(__addtourviewmodel.Destination))
            {
                return true;
            }

            return false;*/
        return true;
        }

        public void Execute(object? parameter)
        {
            Mainlogic.CopyTour(__mainviewmodel.SelectedTour.Name);
            __mainviewmodel.UpdateTours();
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
