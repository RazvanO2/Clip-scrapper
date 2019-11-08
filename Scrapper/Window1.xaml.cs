using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
            if (!File.Exists("Resurse/streamer_list.txt"))
            {
                File.CreateText("Resurse/streamer_list.txt").Close();
            }
            var lines = File.ReadLines("Resurse/streamer_list.txt");
            int a = 0;
                foreach (var line in lines)
                {
                    if (a == 0)
                    {
                        Cutie.AppendText(line);
                        a = 2;
                    }
                    else
                    {
                        Cutie.AppendText("\r" + line);
                    }
                }
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ///
        }

        private void Scrapper_Click(object sender, RoutedEventArgs e)
        {
            Process cmd = new Process();
            ProcessStartInfo argument = new ProcessStartInfo("cmd.exe", "/c cd Resurse && py scrapper.py && pause");
            cmd.StartInfo = argument;
            cmd.Start();
            cmd.WaitForExit();
        }

        private void Downloader_Click(object sender, RoutedEventArgs e)
        {
            Process cmd = new Process();
            ProcessStartInfo argument = new ProcessStartInfo("cmd.exe", "/c cd Resurse && py downloader.py && pause");
            cmd.StartInfo = argument;
            cmd.Start();
            cmd.WaitForExit();
        }

        private void Salveaza_Click(object sender, RoutedEventArgs e)
        {
            string final = new TextRange(Cutie.Document.ContentStart, Cutie.Document.ContentEnd).Text;
            File.WriteAllText("Resurse/streamer_list.txt", final);
        }
    }
}
