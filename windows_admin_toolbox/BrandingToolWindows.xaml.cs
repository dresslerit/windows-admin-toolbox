using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaktionslogik für NetworkTools.xaml
    /// </summary>
    public partial class BrandingToolWindow
    {
        public BrandingToolWindow()
        {
            InitializeComponent();
            updateOEM();
            if(manufactOut.Content != null && modelOut.Content != null)
            {
                newModel.Text = manufactOut.Content.ToString() + ", " + modelOut.Content.ToString();
            }
            brandBtn.Click += brand;
        }
        private void brand(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            Utilities.Extract("windows_admin_toolbox", @"C:\Windows\Branding", "Logo", "logo.bmp");

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "HelpCustomized", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportHours", "09.00 - 19.00 Uhr");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Manufacturer", "someCompany");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportPhone", "someNumber");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportURL", "https://support.securecircle.de");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Model", newModel.Text.ToString());
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Logo", "C:\\Windows\\Branding\\logo.bmp");
            updateOEM();
        }
     
        private void updateOEM ()
        {
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "HelpCustomized", null) != null)
            {
                helpCustOut.Content = ("" + (Int32)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "HelpCustomized", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportHours", null) != null)
            {
                supportHourOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportHours", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Manufacturer", null) != null)
            {
                manufactOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Manufacturer", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportPhone", null) != null)
            {
                supportPhoneOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportPhone", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportURL", null) != null)
            {
                supportURLOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportURL", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Model", null) != null)
            {
                modelOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Model", null));
            }
            if(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Logo", null) != null)
            {
                logoOut.Content = ((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Logo", null));
            }
            
        }
        
    }
}
