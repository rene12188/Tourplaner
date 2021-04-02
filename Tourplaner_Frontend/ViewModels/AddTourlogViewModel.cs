using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tourplaner_Frontend.Commands;
using System.Windows.Controls;

namespace Tourplaner_Frontend
{

    class AddTourlogViewModel : INotifyPropertyChanged
    {
      
        public ICommand Submittourlog
        {
            get;
            set;
        }

    public AddTourlogViewModel()
        {
            Submittourlog = new SubmitTourlog(this);
        }

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
