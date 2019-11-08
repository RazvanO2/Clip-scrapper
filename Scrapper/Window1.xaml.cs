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
using System.IO;
using System.Net;
using System.Diagnostics;
namespace Scrapper
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private void Streamer_v(object sender, RoutedEventArgs e)
        {
            int a = 0;
            string fisier;
            var lines = File.ReadLines("Resurse/streamer_list.txt");

            foreach (var line in lines)
            {
                if( a == 0)
                {
                    Cutie.AppendText(line);
                    a = 1;
                }
                else
                {
                    Cutie.AppendText("\r" + line);
                }
                
            }

  // Process line
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Scrapper_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Downloader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Salveaza_Click(object sender, RoutedEventArgs e)
        {
            string final = new TextRange(Cutie.Document.ContentStart, Cutie.Document.ContentEnd).Text;
            File.WriteAllText("Resurse/salvat.txt", final);
        }
    }
}
