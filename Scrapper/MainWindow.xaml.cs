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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Verificare(object sender, RoutedEventArgs e)
        {
            string fisier;
            Process cmd = new Process();
            string[] linkuri = { "https://gist.githubusercontent.com/Far0/78beb7eed6c5f9ca51ab0569f53ee320/raw", "https://gist.githubusercontent.com/Far0/f7539ccf65cfe3b8f396b8bdd98d7cc6/raw", "https://gist.githubusercontent.com/Far0/6b6c8dc6e614d6416d46f1dc1ff3708f/raw", "https://gist.githubusercontent.com/Far0/d8cb0af2bed0284b00bcd1634f36b1d6/raw", "https://gist.githubusercontent.com/Far0/581356ead6fa2b813ef7557fc95b9641/raw" };
            /// linkuri(0) - versiune script; linkuri(1) - versiune aplicatie; linkuri(2) updater; linkuri(3) scrapper; linkuri(4) downloader
            double[] versiune = new double[2];
            ///versiune(0) - script; versiune(1) aplicatie;
            string[] scripturi = new string[3];
            string[] scripturi_nume = { "updater", "scrapper", "downloader" };
            string[] modul = { "requests", "dpath", "xlwt" };
            string locatie = "Resurse/";
            ProcessStartInfo argument = new ProcessStartInfo("cmd.exe", "/c py -m pip install " + modul[0] + " > " + modul[0] + ".info && py -m pip install " + modul[1] + " > " + modul[1] + ".info && py -m pip install " + modul[2] + " > " + modul[2] + ".info");
            WebClient web = new WebClient();
            Label3.Content = "Versiune aplicație: " + Properties.Settings.Default.aplicatie + Environment.NewLine + "Versiune script-uri: " + Properties.Settings.Default.scripturi;
            System.IO.Directory.CreateDirectory("Resurse");
            for (int i = 0; i <= linkuri.Length - 1; i++)
            {
                StreamReader browser = new StreamReader(web.OpenRead(linkuri[i]));
                if (1 >= i)
                {
                    double z = double.Parse(browser.ReadToEnd());
                    versiune[i] = z;
                }
                else
                {
                    string zz = browser.ReadToEnd();
                    scripturi[i - 2] = zz;

                }
                if (i == linkuri.Length - 1)
                {
                    browser.Close();
                }
            }
            if (Properties.Settings.Default.testul_1 == 0)
            {
                Process.Start("cmd", "/c py --version > python_versiune.info");
                System.Threading.Thread.Sleep(1500);
                fisier = File.ReadAllText("python_versiune.info");
                if (fisier.Contains("Python"))
                {
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este deja instalat.");
                    Properties.Settings.Default.testul_1 = 1;
                    File.Delete("python_versiune.info");
                }
                else
                {
                    Process.Start("https://www.python.org/ftp/python/3.7.4/python-3.7.4.exe");
                    MessageBox.Show("[STAGE 1] Nu ai Python 3.7 instalat!");
                    Close();

                }
            }
            else
            {
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 1] Python este deja instalat.");
            }

            if (Properties.Settings.Default.aplicatie >= versiune[1])
            {
                if (Properties.Settings.Default.aplicatie > versiune[1])
                {
                    RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai o versiune BETA de Night Scrapper.");
                    Properties.Settings.Default.BETA = 1;
                }
                else
                {
                    RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Ai deja ultima versiune de Night Scrapper.");
                }

            }
            else
            {
                MessageBox.Show("Apasă ok pentru a descărca ultima versiune de Night Scrapper.", "Night Scrapper");
                if (File.Exists(scripturi_nume[0] + ".py"))
                {
                    fisier = File.ReadAllText(scripturi_nume[0] + ".py");
                    if (!(scripturi[0] == fisier))
                    {
                        File.Delete(scripturi_nume[0] + ".py");
                        File.WriteAllText(scripturi_nume[0] + ".py", scripturi[0]);
                        Process.Start("cmd", "/c py " + scripturi_nume[0] + ".py");
                        Close();
                    }
                }
                else
                {
                    RichTextBox1.AppendText(Environment.NewLine + "[UPDATER] Nu am gasit " + scripturi_nume[0] + ".py, se descarcă.");
                    File.WriteAllText(scripturi_nume[0] + ".py", scripturi[0]);
                    Process.Start("cmd", "/c py " + scripturi_nume[0] + ".py");
                    Close();
                }
            }
            if ((Properties.Settings.Default.testul_1==1) & (Properties.Settings.Default.testul_2==0))
            {
                cmd.StartInfo = argument;
                cmd.Start();
                cmd.WaitForExit();
                for (int i = 0; i < modul.Length; i++)
                {
                    fisier = File.ReadAllText(modul[i] + ".info");
                    if (fisier.StartsWith("Requirement"))
                    {
                        RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " + modul[i] + " este deja instalat.");
                    }
                    else
                    {
                        RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Modulul " + modul[i] + " s-a instalat.");
                    }
                    try
                    {
                        File.Delete(modul[i] + ".info");
                    }
                    catch
                    {
                        RichTextBox1.AppendText(Environment.NewLine + "[INFO] Nu am găsit log file-ul " + modul[i] + ".info.");
                    }
                    if (i == modul.Length -1)
                    {
                        Properties.Settings.Default.testul_2 = 1;
                    }          
                }
            }
            else
            {
                RichTextBox1.AppendText(Environment.NewLine + "[STAGE 2] Toate modulele necesare sunt deja instalate.");
            }
            for (int i = 1;i<=2;i++)
            {
                if (File.Exists(locatie + scripturi_nume[i] + ".py")){
                    fisier = File.ReadAllText(locatie + scripturi_nume[i] + ".py");
                    if (!(fisier == scripturi[i]))
                    {
                        File.Delete(locatie + scripturi_nume[i] + ".py");
                        File.WriteAllText(locatie + scripturi_nume[i] + ".py", scripturi[i]);
                    }
                    else
                    {
                        RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] " + scripturi_nume[i] + ".py corespunde cu cea mai recentă versiune.");
                    }
                }
                else
                {
                    RichTextBox1.AppendText(Environment.NewLine + "[STAGE 3] Nu am găsit " + scripturi_nume[i] + ".py, se descarcă...");
                    File.WriteAllText(locatie + scripturi_nume[i] + ".py", scripturi[i]);
                }

            }
            Properties.Settings.Default.scripturi = versiune[0];
            Label3.Content = "Versiune aplicație: " + Properties.Settings.Default.aplicatie + Environment.NewLine + "Versiune script-uri: " + Properties.Settings.Default.scripturi;
            if (Properties.Settings.Default.BETA == 1)
            {
                Button1.Visibility = Visibility.Visible;
            }
            /// string lol = Properties.Settings.Default.aplicatie.ToString();
            ///  MessageBox.Show(lol);
        }
        private void Redeschide_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window1();
            window.Show();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            RichTextBox1.AppendText(Environment.NewLine + Properties.Settings.Default.testul_1 + Environment.NewLine + Properties.Settings.Default.testul_2);
        }
    }
}
