using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace FileManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReportService reportService = new ReportService();
        public MainWindow()
        {
            InitializeComponent();
            ShowPath("C:\\"); // example
            GetDiskSpace();
           
            
        }
        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var GetPath = ((System.Windows.Controls.ContentControl)sender).Content;
            string Paths = Convert.ToString(GetPath);
            if (Path.GetExtension(Paths) == "")
            {
                ShowPath(Paths);
            }
            else
            {
                string a = PathText.Text;
                Paths = a + Paths;
                Process.Start(Paths);
            }

        }

        private void ShowPath(string path)
        {
            PathText.Text = path;
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles("*.*");
            FileView.Items.Clear();

            foreach (FileInfo fi in files)
            {
                FileView.Items.Add(fi);
            }
            foreach (string subDir in Directory.GetDirectories(Convert.ToString(dir)))
            {
                FileView.Items.Add(subDir);
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            string Path = PathText.Text;
            if (Path!="C:\\")
            {
                char c = Path[Path.Length - 1];
                if (c == '\\')
                {
                    Path = Path.Remove(Path.Length - 1, 1);
                }

                for (int i = Path.Length - 1; i < Path.Length; i--)
                {
                    c = Path[Path.Length - 1];

                    if (c == '\\')
                    {
                        break;
                    }
                    Path = Path.Remove(Path.Length - 1, 1);
                }
                PathText.Text = Path;
                ShowPath(Path);
            }
        }
        private void GetDiskSpace()
        {
            DiskUtility diskUtility = new DiskUtility();
            DriveInfo CDrive = new DriveInfo("C");
            diskUtility.PlaceLeft = CDrive.AvailableFreeSpace/1024/1024/1024; 
            diskUtility.DiskSize = CDrive.TotalSize/1024/1024/1024;
            DiskInfo.Content = reportService.GenerateReport(diskUtility);


        }
    }




}
