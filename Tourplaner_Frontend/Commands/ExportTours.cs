using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using Tourplaner_Buisness;
using MessageBox = System.Windows.MessageBox;

namespace Tourplaner_Frontend.Commands
{
    class ExportTours : ICommand
    {
        private readonly MainViewModel __mainviewmodel = null;
        public ExportTours(MainViewModel tmp)
        {
            this.__mainviewmodel = tmp;

        }

        public bool CanExecute(object? parameter)
        {

        return true;
        }

        public void Execute(object? parameter)
        {
            FolderBrowserDialog tmp = new FolderBrowserDialog();
            if (tmp.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(tmp.SelectedPath);
                this.__mainviewmodel.CreatePDF(tmp.SelectedPath);
            }
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
