﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Tourplaner_Buisness;
using Tourplaner_Utility;
using MessageBox = System.Windows.MessageBox;

namespace Tourplaner_Frontend.Commands
{
    class CreateSummary : ICommand, INotifyPropertyChanged
    {
        private readonly MainViewModel _mainviewModel = null;
        public CreateSummary(MainViewModel tmp)
        {
            this._mainviewModel = tmp;
            _mainviewModel.PropertyChanged += (sender, args) =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            };

        }

        public bool CanExecute(object? parameter) => true;

        public async void  Execute(object? parameter)
        {
            FolderBrowserDialog tmp = new FolderBrowserDialog();
         
            try
            {
                if (tmp.ShowDialog() == DialogResult.OK)
                {
                    PdfWriter.CreatePdfSummary(tmp.SelectedPath, _mainviewModel.Tourlist);
                }
              
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler? CanExecuteChanged;
    }
}
