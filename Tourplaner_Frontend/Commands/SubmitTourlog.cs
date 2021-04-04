using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplaner_Buisness;

namespace Tourplaner_Frontend.Commands
{
    class SubmitTourlog: ICommand
    {
        private readonly AddTourlogViewModel __addtourviewmodel = null;
        private readonly string SelectedTour = null;

        public SubmitTourlog(AddTourlogViewModel tmp, string selectedtourname)
        {
            this.SelectedTour = selectedtourname;
            this.__addtourviewmodel = tmp;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            try
            {
                Tourlog tmp = new Tourlog(__addtourviewmodel.Time, __addtourviewmodel.Report, __addtourviewmodel.Distance, __addtourviewmodel.TTime, __addtourviewmodel.Rating, __addtourviewmodel.Difficulty, __addtourviewmodel.Temp);
                Mainlogic.Inserttourlog(tmp, MainViewModel.PublicselectedTour.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

            if (parameter != null && parameter is Window)
            {
                ((Window)parameter).Close();
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
