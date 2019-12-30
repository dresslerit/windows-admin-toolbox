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
    public partial class NetworkToolWindow
    {
        public NetworkToolWindow()
        {
            InitializeComponent();
            adapter_info.Click += adapterInfo;
            ping_google.Click += pingGoogle;
            ping_local.Click += pingLocal;
            flush_dns.Click += flushDns;
            arp_info.Click += arpInfo;
            arp_clear.Click += arpClear;
            dhcp_renew.Click += dhcpRenew;
            dhcp_register.Click += dhcpRegister;
            dhcp_release.Click += dhcpRelease;
            adapter_reset.Click += adapterReset;
        }
        private void adapterInfo(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("ipconfig /all", "Netzwerk Adapter Informationen", "");
        }
        private void pingGoogle(object sender, EventArgs e)
        {

            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("ping google.de", "Ping Test google.de", "");
        }
        private void pingLocal(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("ping localhost", "Ping Test localhost", "");
        }
        private void flushDns(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("ipconfig /flushdns", "DNS Cache Löschen", "");
        }
        private void arpInfo(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("arp -a", "ARP Information", "");
        }
        private void arpClear(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("arp -d", "ARP Leeren", "");
        }
        private void dhcpRenew(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            String tmp = "";
            tmp += "ipconfig /renew *\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo DHCP Renew IPv6 \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "ipconfig /renew6 *\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";
            mw.buildCMD(tmp, "DHCP Renew IPv4", "");
        }
        private void dhcpRegister(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("ipconfig /registerdns *", "DHCP DNS Registrieren", "");
        }
        private void dhcpRelease(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            String tmp = "";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo DHCP Release IPv6 \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "ipconfig /release6 *\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";
            mw.buildCMD("ipconfig /release *", "DHCP Release IPv4", "");
        }
        private void adapterReset(object sender, EventArgs e)
        {
            MainWindow mw = Owner as MainWindow;
            mw.buildCMD("netsh winsock reset \n netsh int ip reset ", "Netzwerk Adapter Reset", "");
        }

        private void dhcp_register_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
