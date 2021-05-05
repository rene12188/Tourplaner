using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tourplaner_Frontend.Annotations;

namespace Tourplaner_Frontend
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public double Distance
        {
            get
            {
                return (double)this.GetValue(ImageBytesProperty);
            }

            set
            {
                this.SetValue(ImageBytesProperty, value);
                this.OnPropertyChanged(nameof(Distance));
            }
        }
        public static readonly DependencyProperty ImageBytesProperty =
            DependencyProperty.Register(
                "Distance",
                typeof(double),
                typeof(UserControl1),
                new FrameworkPropertyMetadata(DistanceValueChangedCallBack));

        private static void DistanceValueChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            UserControl1 property = sender as UserControl1;
            property.Distancelabel.Content = args.NewValue;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
