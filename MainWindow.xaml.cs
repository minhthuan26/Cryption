using MaHoa.ControlView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private object _controlView;
        public object ControlView { get { if (_controlView == null) _controlView = new Ceasar(); return _controlView; } set { _controlView = value; } }
        private string _plainText;
        public string PlainText 
        { 
            get 
            {
                _plainText = plainText.Text;
                return _plainText; 
            } 
            set 
            { 
                _plainText = value;
                plainText.Text = _plainText;
            } 
        }
        private string _cipherText;
        public string CipherText
        {
            get
            {
                _cipherText = cipherText.Text;
                return _cipherText;
            }
            set
            {
                _cipherText = value;
                cipherText.Text = _cipherText;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            //OpenFileDialog openFile = new OpenFileDialog();
            //openFile.ShowDialog();
            //string a = File.ReadAllText(openFile.FileName).Trim('\r', '\n', ' ');
            //while(a.Contains('\r') || a.Contains('\n'))
            //    a.Remove
            //string[] b = a.Split(" ");
        }
        
        private void Load(object sender, RoutedEventArgs e)
        {
            ControlView = new Ceasar();
            controlContent.Content = ControlView;
            cipherType.Text = "Ceasar";
        }
        
        private void Ceasar(object sender, RoutedEventArgs e)
        {
            ControlView = new Ceasar();
            controlContent.Content = ControlView;
            cipherType.Text = "Ceasar";
        }

        private void Affine(object sender, RoutedEventArgs e)
        {
            ControlView = new Affine();
            controlContent.Content = ControlView;
            cipherType.Text = "Affine";
        }

        private void Vigenere(object sender, RoutedEventArgs e)
        {
            ControlView = new Vigenere();
            controlContent.Content = ControlView;
            cipherType.Text = "Vigenere";
        }

        private void Hill(object sender, RoutedEventArgs e)
        {
            ControlView = new Hill();
            controlContent.Content = ControlView;
            cipherType.Text = "Hill";
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            string cipher = cipherType.Text;
            switch (cipher)
            {
                case "Ceasar":
                    int ceasarKey;
                    Ceasar ceasar = (Ceasar) ControlView;
                    ceasar.DecodeKey = "";
                    try
                    {
                        ceasarKey = Int32.Parse(ceasar.EncodeKey);
                        CipherText = Cryption.Ceasar.Encode(PlainText, ceasarKey);
                    }
                    catch (FormatException)
                    {
                        CipherText = "";
                        MessageBox.Show("Khoá phải là số nguyên.");
                    }
                    break;

                case "Affine":
                    int affineKey1, affineKey2;
                    Affine affine = (Affine)ControlView;
                    affine.DecodeKey1 = "";
                    affine.DecodeKey2 = "";
                    try
                    {
                        affineKey1 = Int32.Parse(affine.EncodeKey1);
                        affineKey2 = Int32.Parse(affine.EncodeKey2);
                        CipherText = Cryption.Affine.Encode(PlainText, affineKey1, affineKey2);
                    }
                    catch (FormatException)
                    {
                        CipherText = "";
                        MessageBox.Show("Khoá phải là số nguyên.");
                    }
                    break;

                case "Vigenere":
                    Vigenere vigenere = (Vigenere)ControlView;
                    vigenere.DecodeKey = "";
                    string vigenereKey = vigenere.EncodeKey;
                    if (vigenereKey.Length == 0)
                    {
                        CipherText = "";
                        MessageBox.Show("Khoá không được để trống.");
                    }
                    else
                        CipherText = Cryption.Vigenere.Encode(PlainText, vigenereKey);
                    break;
            }
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            PlainText = "";
            CipherText = "";
            string cipher = cipherType.Text;
            switch (cipher)
            {
                case "Ceasar":
                    Ceasar ceasar = (Ceasar)ControlView;
                    ceasar.EncodeKey = ceasar.DecodeKey = "";
                    break;

                case "Affine":
                    Affine affine = (Affine)ControlView;
                    affine.EncodeKey1 = affine.EncodeKey2 = affine.DecodeKey1 = affine.DecodeKey2 = "";
                    break;

                case "Vigenere":
                    Vigenere vigenere = (Vigenere)ControlView;
                    vigenere.EncodeKey = vigenere.DecodeKey = "";
                    break;
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            string cipher = cipherType.Text;
            switch (cipher)
            {
                case "Ceasar":
                    int key;
                    Ceasar ceasar = (Ceasar)ControlView;
                    ceasar.EncodeKey = "";
                    try
                    {
                        key = Int32.Parse(ceasar.DecodeKey);
                        PlainText = Cryption.Ceasar.Decode(CipherText, key);
                    }
                    catch (FormatException)
                    {
                        PlainText = "";
                        MessageBox.Show("Khoá phải là số nguyên.");
                    }
                    break;

                case "Affine":
                    int key1, key2;
                    Affine affine = (Affine)ControlView;
                    affine.EncodeKey1 = "";
                    affine.EncodeKey2 = "";
                    try
                    {
                        key1 = Int32.Parse(affine.DecodeKey1);
                        key2 = Int32.Parse(affine.DecodeKey2);
                        PlainText = Cryption.Affine.Decode(CipherText, key1, key2);
                    }
                    catch (FormatException)
                    {
                        PlainText = "";
                        MessageBox.Show("Khoá phải là số nguyên.");
                    }
                    break;

                case "Vigenere":
                    Vigenere vigenere = (Vigenere)ControlView;
                    vigenere.EncodeKey = "";
                    string vigenereKey = vigenere.DecodeKey;
                    if (vigenereKey.Length == 0)
                    {
                        PlainText = "";
                        MessageBox.Show("Khoá không được để trống.");
                    }
                    else
                        PlainText = Cryption.Vigenere.Decode(CipherText, vigenereKey);
                    break;
            }
        }
    }
}
