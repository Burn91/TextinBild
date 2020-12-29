using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bildfilter
{
   
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString();
        public MainWindow()
        {
            InitializeComponent();
            
        }


        int breite = 1000;
        string pfad;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string s = txbox.Text;
            double laenge = s.Length;
            double reihen = laenge / breite;

            if (reihen < 1)
            {
                reihen = 1;
            }
            else
            {
                reihen = Math.Ceiling(reihen);
            }
            int r = (int)reihen;


            Bitmap image = new Bitmap(breite, r);
            byte[] asciibytes = Encoding.ASCII.GetBytes(s);
            int z = 0;
            int cc = 0;

            for (int y = 0; y < r; y++)
            {
                for (int x = 0; x < breite; x++)
                {
                    if (z == s.Length)
                    {
                        break;
                    }
                    if (cc == 0)
                    {
                        Color newColor = Color.FromArgb(asciibytes[z], 0, 0);
                        image.SetPixel(x, y, newColor);
                        cc++;

                    }
                    else if (cc == 1)
                    {
                        Color newColor = Color.FromArgb(0, asciibytes[z], 0);
                        image.SetPixel(x, y, newColor);
                        cc++;

                    }
                    else
                    {
                        Color newColor = Color.FromArgb(0, 0, asciibytes[z]);
                        image.SetPixel(x, y, newColor);
                        cc = 0;

                    }

                    z++;

                }
            }
            MessageBox.Show(path);
            image.Save(path + "\\bild.jpg");
            txbox.Clear();
        
        }


        private void btnLesen_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                 pfad = openFileDialog.FileName.ToString();

            }
            

            Bitmap image = new Bitmap(pfad);
            byte[] b = new byte[breite*image.Height];
            int z = 0;
            int cc = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < breite; x++)
                {
                    if(cc==0)
                    {
                        Color oc = image.GetPixel(x, y);
                        b[z] = Convert.ToByte(oc.R);
                        z++;
                        cc++;
                    }
                    else if(cc == 1)
                    {
                        Color oc = image.GetPixel(x, y);
                        b[z] = Convert.ToByte(oc.G);
                        z++;
                        cc++;
                    }
                    else
                    {
                        Color oc = image.GetPixel(x, y);
                        b[z] = Convert.ToByte(oc.B);
                        z++;
                        cc = 0;
                    }
                   
                }
            }

            string result = System.Text.Encoding.UTF8.GetString(b);
            txbox.Text = result;

        }

      
    }
}
