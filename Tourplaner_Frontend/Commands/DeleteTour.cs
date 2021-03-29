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
    class DeleteTour : ICommand
    {
        private readonly MainViewModel _mainviewModel = null;
        public DeleteTour(MainViewModel tmp)
        {
            this._mainviewModel = tmp;

        }

        public bool CanExecute(object? parameter)
        {

        return true;
        }

        public void Execute(object? parameter)
        {
          
            try
            {
                Mainlogic.DeleteTour(_mainviewModel.SelectedTour.getName());

            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
