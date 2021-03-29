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
    class SubmitTour : ICommand
    {
        private readonly AddTourViewModel __addtourviewmodel = null;
        public SubmitTour(AddTourViewModel tmp)
        {
            this.__addtourviewmodel = tmp;

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
            if (string.IsNullOrWhiteSpace(__addtourviewmodel.Tourname))
            {
                System.Windows.MessageBox.Show("Please Enter a Tour Name ");
                return;
            }
            else if (string.IsNullOrWhiteSpace(__addtourviewmodel.Source))
            {
                System.Windows.MessageBox.Show("Please Enter a Source ");
                return;
            }
            else if (string.IsNullOrWhiteSpace(__addtourviewmodel.Destination))
            {
                System.Windows.MessageBox.Show("Please Enter a Destination ");
                return;
            }

            try
            {
                Debug.Write("Creating TOur \n");
                Tour tmp = new Tour(__addtourviewmodel.Tourname, __addtourviewmodel.Description,
                    __addtourviewmodel.Source,__addtourviewmodel.Description,__addtourviewmodel.Distance);
                Mainlogic.SaveTour(tmp);

            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
            
            if (parameter != null && parameter is Window)
            { 
                ((Window)parameter).Close();
            }
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
