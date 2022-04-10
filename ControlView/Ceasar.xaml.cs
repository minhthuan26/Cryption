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

namespace MaHoa.ControlView
{
    /// <summary>
    /// Interaction logic for Ceasar.xaml
    /// </summary>
    public partial class Ceasar : UserControl, INotifyPropertyChanged
    {
        private string _encodeKey;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string EncodeKey { get {_encodeKey = encodeKey.Text; return _encodeKey; } set { _encodeKey = value; OnPropertyChanged(); } }

        public Ceasar()
        {
            InitializeComponent();
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            EncodeKey = encodeKey.Text;
        }

        private void getPlainText(object sender, TextChangedEventArgs e)
        {
            //EncodeKey = encodeKey.Text;
        }
    }
}
