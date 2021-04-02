using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tourplaner_Frontend.Commands
{
    class SubmitTourlog: ICommand
    {
        private readonly AddTourlogViewModel __addtourviewmodel = null;

        public SubmitTourlog(AddTourlogViewModel tmp)
        {
            this.__addtourviewmodel = tmp;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            

            if (parameter != null && parameter is Window)
            {
                ((Window)parameter).Close();
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
