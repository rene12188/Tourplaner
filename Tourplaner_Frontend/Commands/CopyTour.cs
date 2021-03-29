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
           /*
            try
            {
                Debug.Write("Creating TOur \n");
                Tour tmp = new Tour(__addtourviewmodel.Tourname, __addtourviewmodel.Source,
                    __addtourviewmodel.Destination);
                Mainlogic.SaveTour(tmp);

            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
            
            this.__addtourviewmodel.Closwindow();
            if (parameter != null && parameter is Window)
            { 
                ((Window)parameter).Close();
            }*/
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
