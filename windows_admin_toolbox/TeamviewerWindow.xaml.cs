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
using System.Windows.Shapes;
using MahApps.Metro;

namespace windows_admin_toolbox
{
    /// <summary>
    /// Interaktionslogik für NetworkTools.xaml
    /// </summary>
    public partial class TeamviewerWindow
    {
        public TeamviewerWindow()
        {
            InitializeComponent();
            host.Click += hostD;
           
            qs.Click += qsD;
           
        }

        private void hostD(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://get.teamviewer.com/secure-circle_host");
        }

        private void qsD(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://get.teamviewer.com/secure-circle_ems");
        }
    }
}
