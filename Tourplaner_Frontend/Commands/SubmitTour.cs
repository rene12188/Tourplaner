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
            __addtourviewmodel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed of Input");
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            };
        }

        public bool CanExecute(object? parameter)
        {
            //return true;
            if (!string.IsNullOrWhiteSpace(__addtourviewmodel.Tourname) && !string.IsNullOrWhiteSpace(__addtourviewmodel.Source) &&
                !string.IsNullOrWhiteSpace(__addtourviewmodel.Description)&& !string.IsNullOrWhiteSpace(__addtourviewmodel.Destination))
            {
                return true;
            }

            return false;
        
        }

        public async void Execute(object? parameter)
        {
            try
            {
                Debug.Write("Creating TOur \n");
                Tour tmp = new Tour(null, __addtourviewmodel.Tourname, __addtourviewmodel.Description,
                    __addtourviewmodel.Source, __addtourviewmodel.Destination, __addtourviewmodel.Distance);

                var t = Mainlogic.SaveTour(tmp);
                await t;
                
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
