using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Affine.xaml
    /// </summary>
    public partial class Affine : UserControl
    {
        private string _encodeKey1;
        public string EncodeKey1
        {
            get
            {
                _encodeKey1 = encodeKey1.Text;
                return _encodeKey1;
            }
            set
            {
                _encodeKey1 = value;
                encodeKey1.Text = _encodeKey1;
            }
        }

        private string _encodeKey2;
        public string EncodeKey2
        {
            get
            {
                _encodeKey2 = encodeKey2.Text;
                return _encodeKey2;
            }
            set
            {
                _encodeKey2 = value;
                encodeKey2.Text = _encodeKey2;
            }
        }

        private string _decodeKey1;

        public string DecodeKey1
        {
            get
            {
                _decodeKey1 = decodeKey1.Text;
                return _decodeKey1;
            }
            set
            {
                _decodeKey1 = value;
                decodeKey1.Text = _decodeKey1;
            }
        }

        private string _decodeKey2;

        public string DecodeKey2
        {
            get
            {
                _decodeKey2 = decodeKey2.Text;
                return _decodeKey2;
            }
            set
            {
                _decodeKey2 = value;
                decodeKey2.Text = _decodeKey2;
            }
        }

        private string _plainTextKeyFile;
        public string PlainTextKeyFile
        {
            get
            {
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == true)
                {
                    _plainTextKeyFile = File.ReadAllText(openFile.FileName);
                }

                return _plainTextKeyFile.ToString().Trim('\r', '\n', ' ');
            }
            set
            {
                _cipherTextKeyFile = value;
            }
        }

        private string _cipherTextKeyFile;
        public string CipherTextKeyFile
        {
            get
            {
                OpenFileDialog openFile = new OpenFileDialog();
                if (openFile.ShowDialog() == true)
                {
                    _cipherTextKeyFile = File.ReadAllText(openFile.FileName);
                }

                return _cipherTextKeyFile.ToString().ToString().Trim('\r', '\n', ' ');
            }
            set
            {
                _cipherTextKeyFile = value;
            }
        }
        public Affine()
        {
            InitializeComponent();
        }

        private void ReadPlainTextKeyFile(object sender, RoutedEventArgs e)
        {
            string[] key = PlainTextKeyFile.Split(" ");
            EncodeKey1 = key[0];
            EncodeKey2 = key[1];
        }

        private void ReadCipherTextKeyFile(object sender, RoutedEventArgs e)
        {
            string[] key = PlainTextKeyFile.Split(" ");
            DecodeKey1 = key[0];
            DecodeKey2 = key[1];
        }
    }
}
