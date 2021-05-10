using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using Tourplaner_Buisness;
using Tourplaner_Utility;
using MessageBox = System.Windows.MessageBox;

namespace Tourplaner_Frontend.Commands
{
    class SetImageFolder : ICommand
    {
        private readonly MainViewModel __mainviewmodel = null;
        public SetImageFolder(MainViewModel tmp)
        {
            this.__mainviewmodel = tmp;

        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            FolderBrowserDialog tmp = new FolderBrowserDialog();
            if (tmp.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(tmp.SelectedPath);
                CFGManager.AddUpdateAppSettings("ImageFolder" , tmp.SelectedPath);
            }
        }
        
        public event EventHandler? CanExecuteChanged;
    }
}
