using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace windows_admin_toolbox
{
    /// <summary>
    /// Interaktionslogik für ConsoleWindow.xaml
    /// </summary>
    public partial class ConsoleWindow : Window
    {
        public Boolean isMainClosed = false;
        public ConsoleWindow()
        {
            InitializeComponent();
        }
        public void refreshLog(String data)
        {
            log.Text += "\n" + data;
            log.ScrollToEnd();
        }
        private void ConsoleWindowClosing(object sender, CancelEventArgs e)
        {
            if(isMainClosed)
            {
                e.Cancel = false;  // Allows the close request
            }
            else
            {
                e.Cancel = true;  // Cancels the close request
            }
        }
        public void MainClose()
        {
            isMainClosed = true;
            this.Close();
        }
    }
}
