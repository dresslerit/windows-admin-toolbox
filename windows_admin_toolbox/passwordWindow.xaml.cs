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

namespace windows_admin_toolbox
{
    /// <summary>
    /// Interaktionslogik für passwordWindow.xaml
    /// </summary>
    public partial class passwordWindow : Window
    {
        public passwordWindow()
        {
            InitializeComponent();
            ok.Click += checkPW;
        }
        private void checkPW(object sender, RoutedEventArgs e)
        {
            if(secretPW.Password.ToString() == "geheim")
            {
                this.Close();
                BrandingToolWindow btW = new BrandingToolWindow();
                //btW.Owner = this;
                btW.ShowDialog();
            }
            else
            {
                this.Close();
            }
        }
    }
}
