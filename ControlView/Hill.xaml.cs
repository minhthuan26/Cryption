using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Hill.xaml
    /// </summary>
    public partial class Hill : UserControl
    {
        public Hill()
        {
            InitializeComponent();
        }
        private void ReadPlainTextKeyFile(object sender, RoutedEventArgs e)
        {
            //string[] key = PlainTextKeyFile.Split(" ");
            //EncodeKey1 = key[0];
            //EncodeKey2 = key[1];
        }

        private void ReadCipherTextKeyFile(object sender, RoutedEventArgs e)
        {
            //string[] key = PlainTextKeyFile.Split(" ");
            //DecodeKey1 = key[0];
            //DecodeKey2 = key[1];
        }
    }
}
