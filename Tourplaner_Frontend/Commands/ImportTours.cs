using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using Tourplaner_Buisness;
using MessageBox = System.Windows.MessageBox;

namespace Tourplaner_Frontend.Commands
{
    class ImportTour : ICommand
    {
        private readonly MainViewModel __mainviewmodel = null;
        public ImportTour(MainViewModel tmp)
        {
            this.__mainviewmodel = tmp;

        }

        public bool CanExecute(object? parameter)
        {

        return true;
        }

        public void Execute(object? parameter)
        {
            OpenFileDialog tmp = new OpenFileDialog();
            if (tmp.ShowDialog() == DialogResult.OK)
            {
                string filePath = tmp.FileName;
                MessageBox.Show(filePath);
                JsonOperator.Import(filePath);
                __mainviewmodel.Tourlist = Mainlogic.UpdateTours();
            }
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
