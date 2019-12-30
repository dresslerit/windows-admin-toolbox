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
using Microsoft.Win32;

namespace windows_admin_toolbox
{
    /// <summary>
    /// Interaktionslogik für PatchDayWindow.xaml
    /// </summary>
    public partial class PatchDayWindow : Window
    {
        public PatchDayWindow()
        {
            InitializeComponent();
            readReg();
            saveBtn.Click += saveAll;
            reset_Btn.Click += resetAll;
        }
        private void resetAll(object sender, RoutedEventArgs e)
        {
            driverOK.IsChecked = false;
            driverUninst.IsChecked = false;
            tvOK.IsChecked = false;
            winUpdate.IsChecked = false;
            toolboxCleanup.IsChecked = false;
            winPrinter.IsChecked = false;
            wiseCleanup.IsChecked = false;
            appUpdate.IsChecked = false;
            officeUpdate.IsChecked = false;
            technican.Text = "";
        }
        private void saveAll(object sender, RoutedEventArgs e)
        {
            if(technican.Text == "")
            {
                MessageBox.Show("Bitte Techniker angeben");
            }
            else
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "driverOK", driverOK.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "driverUninst", driverUninst.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "tvOK", tvOK.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "winUpdate", winUpdate.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "toolboxCleanup", toolboxCleanup.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "winPrinter", winPrinter.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "wiseCleanup", wiseCleanup.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "appUpdate", appUpdate.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "officeUpdate", officeUpdate.IsChecked);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "technican", technican.Text);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "lastMaint", technican.Text + ", " + DateTime.Now);
                readReg();
            }
        }
        private void readReg()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "App", "windows_admin_toolbox");
            lastMaint.Content = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "lastMaint", "").ToString();
            driverOK.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "driverOK", "False").ToString());
            driverUninst.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "driverUninst", "False").ToString());
            tvOK.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "tvOK", "False").ToString());
            winUpdate.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "winUpdate", "False").ToString());
            toolboxCleanup.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "toolboxCleanup", "False").ToString());
            winPrinter.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "winPrinter", "False").ToString());
            wiseCleanup.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "wiseCleanup", "False").ToString());
            appUpdate.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "appUpdate", "False").ToString());
            officeUpdate.IsChecked = Boolean.Parse(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\windows_admin_toolbox", "officeUpdate", "False").ToString());
        }
    }
}
