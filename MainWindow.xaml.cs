using MaHoa.ControlView;
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

namespace MaHoa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl _controlView;
        public UserControl ControlView { get { if (_controlView == null) _controlView = new Ceasar(); return _controlView; } set { _controlView = value; OnPropertyChanged(); } }
        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void getControlView(object sender, RoutedEventArgs e)
        {
            ControlView = new Ceasar();
            controlContent.Content = ControlView;
            cipherName.Text = "Ceasar";
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            ControlView = new Ceasar();
            controlContent.Content = ControlView;
            cipherName.Text = "Ceasar";
        }
    }
}
