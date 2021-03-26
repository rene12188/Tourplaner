using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tourplaner_Utility;

namespace Tourplaner_Frontend.Commands
{
    class SubmitTour : ICommand
    {
        private readonly AddTourViewModel __addtourviewmodel = null;

        public SubmitTour(AddTourViewModel tmp)
        {
            this.__addtourviewmodel = tmp;

        }

        public bool CanExecute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(__addtourviewmodel.Tourname))
            {
                Debug.Write("Valid Send \n");
                return true;
            }

            Debug.Write("invalid Send \n");
            return false;
        }

        public void Execute(object? parameter)
        {
            Debug.Write("Creating TOur \n");
            Tour tmp = new Tour(__addtourviewmodel.Tourname, __addtourviewmodel.Start_longitude, __addtourviewmodel.Start_lattitude, __addtourviewmodel.Finish_longitude, __addtourviewmodel.Finish_lattitude);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
