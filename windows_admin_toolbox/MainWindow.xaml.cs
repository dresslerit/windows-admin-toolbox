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
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace windows_admin_toolbox
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public string logs ="";
        private ConsoleWindow conWin = new ConsoleWindow();
        public MainWindow()
        {
            InitializeComponent();
            sys_cmd.Click += sysCMD;
            dism_repair.Click += dismRepair;
            dism_clean.Click += dismClean;
            sfc.Click += sfcRun;
            chkdsk_fr.Click += chkdskFr;
            dism_check.Click += dismCheck;
            dism_scan.Click += dismScan;
            chkdsk_active.Click += chkdskActive;
            wsus_reset.Click += wsusReset;
            wsus_repair.Click += wsusDiag;
            gpo_update.Click += gpoUpdate;
            gpo_debug.Click += gpoDebug;
            sys_clean.Click += sysClean;
            //sys_patch.Click += sysPatch;
            god_Mode.Click += godMode;
            sys_brand.Click += sysBrand;
            sys_Perfmon.Click += sysPerfmon;
            network_tools.Click += networkTools;
            sys_control.Click += sysControl;
            sys_services.Click += sysServices;
            sys_privacy.Click += sysPrivacy;
            prog_driver.Click += progDriver;
            prog_registry.Click += progRegistry;
            prog_defrag.Click += progDefrag;
            prog_teamviewer.Click += progTeamviewer;
            patchday.Click += patchDayList;
            SuppressStandby();
            openConsoleWindow();
        }

        private void WindowClosing(object sender, EventArgs e)
        {
            EnableStandby();
            conWin.MainClose();
            MessageBoxResult m = MessageBox.Show("Log Speichern?", "Log Speichern", MessageBoxButton.OKCancel);
            if (m == MessageBoxResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Log Datei (*.log)|*.log|Text Datei (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, logs);
                }
                    
            }
        }
        private void sysCMD(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd");
        }
        private void sysPerfmon(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("perfmon", "/report");
        }
        private void sysPrivacy(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Noch leider nicht verfügbar...");
        }
        private void dismRepair(object sender, RoutedEventArgs e)
        {
            buildCMD("dism /online /cleanup-image /restorehealth", "DISM Restore Health", "");
        }
        private void dismClean(object sender, RoutedEventArgs e)
        {
            buildCMD("dism /online /cleanup-image /startcomponentcleanup", "DISM Component Store Cleanup", "");
        }
        private void sfcRun(object sender, RoutedEventArgs e)
        {
            runCMD("sfc /scannow", "System File Check", "Bitte System neustarten!");
        }
        private void chkdskFr(object sender, RoutedEventArgs e)
        {
            buildCMD("echo J | chkdsk C: /F /R", "Festplatte reparieren und Sektoren pruefen", "Bitte System Neustarten!");
        }
        private void dismCheck(object sender, RoutedEventArgs e)
        {
            buildCMD("dism /online /cleanup-image /checkhealth", "DISM Check Health", "");
        }
        private void dismScan(object sender, RoutedEventArgs e)
        {
            buildCMD("dism /online /cleanup-image /scanhealth", "DISM Scan Health", "");
        }
        private void chkdskActive(object sender, RoutedEventArgs e)
        {
            buildCMD("chkdsk C: /scan", "Festplatte Aktiv Pruefen", "");
        }
        private void wsusReset(object sender, RoutedEventArgs e)
        {
            buildCMD("wuauclt /resetauthorization /detectnow", "WSUS Client Reset", "");
        }
        private void wsusDiag(object sender, RoutedEventArgs e)
        {
            Utilities.Extract("windows_admin_toolbox", System.IO.Path.GetTempPath(), "Resources", "wu170509.diagcab");
            System.Diagnostics.Process.Start("msdt.exe", @"/cab " + System.IO.Path.GetTempPath() + @"\wu170509.diagcab");
        }
        private void godMode(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "shell:::{ED7BA470-8E54-465E-825C-99712043E01C}");
        }
        private void gpoUpdate(object sender, RoutedEventArgs e)
        {
            runCMD("gpupdate /force", "Group Policy Force Update", "");
        }
        private void gpoDebug(object sender, RoutedEventArgs e)
        {
            String tmp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".html";
            runCMD("gpupdate /force \n" + "gpresult /h \"" + tmp + "\"\n\"" + tmp + "\"", "Group Policy Debug", "");
        }
        private void sysClean(object sender, RoutedEventArgs e)
        {
            String tmp = "";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Active Setup Temp Folders\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\BranchCache\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Content Indexer Cleaner\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Delivery Optimization Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Device Driver Packages\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Downloaded Program Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameNewsFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameStatisticsFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameUpdateFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Internet Cache Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Offline Pages Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Old ChkDsk Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Previous Installations\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Recycle Bin\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\RetailDemo Offline Content\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Service Pack Cleanup\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Setup Log Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\System error memory dump files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\System error minidump files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Setup Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Sync Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Thumbnail Cache\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Thumbnail Caches\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Update Cleanup\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Upgrade Discarded Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\User file versions\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Defender\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Archive Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Queue Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting System Archive Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting System Queue Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Temp Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows ESD installation files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Upgrade Log Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "cleanmgr /sagerun:6555 \n";
            tmp += "del /F /S /Q %temp%\\ \n ";
            buildCMD(tmp, "System Cleanup", "");
        }
        private void sysPatch(object sender, RoutedEventArgs e)
        {
            String tmp = "";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Starte DISM StartComponentCleanup\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "dism /online /cleanup-image /startComponentCleanup\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";

            tmp += "echo -----------------------------------\n ";
            tmp += "echo Starte DISM Check Health\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "dism /online /cleanup-image /checkhealth\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";

            
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Starte DISM Scan Health\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "dism /online /cleanup-image /scanhealth\n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";

            tmp += "echo -----------------------------------\n ";
            tmp += "echo Starte System Cleanup\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo.\n ";
            tmp += "echo.\n ";
           
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Active Setup Temp Folders\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\BranchCache\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Content Indexer Cleaner\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Delivery Optimization Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Device Driver Packages\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Downloaded Program Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameNewsFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameStatisticsFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\GameUpdateFiles\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Internet Cache Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Offline Pages Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Old ChkDsk Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Previous Installations\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Recycle Bin\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\RetailDemo Offline Content\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Service Pack Cleanup\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Setup Log Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\System error memory dump files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\System error minidump files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Setup Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Temporary Sync Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Thumbnail Cache\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Thumbnail Caches\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Update Cleanup\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Upgrade Discarded Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\User file versions\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Defender\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Archive Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Queue Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting System Archive Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting System Queue Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Error Reporting Temp Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows ESD installation files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\VolumeCaches\\Windows Upgrade Log Files\" /v StateFlags6555 /t REG_DWORD /d 00000002 /f \n ";
            tmp += "cleanmgr /sagerun:6555 \n";
            tmp += "del /F /S /Q %temp%\\ \n ";
            tmp += "echo.\n ";
            tmp += "echo -----------------------------------\n ";
            tmp += "echo Abgeschlossen! \n ";
            tmp += "echo -----------------------------------\n ";
            buildCMD(tmp, "PatchDay Cleanup", "");
                    
        }
        private void sysBrand(object sender, RoutedEventArgs e)
        {
            passwordWindow pwW = new passwordWindow();
            pwW.Owner = this;
            pwW.ShowDialog();

        }
        private void openConsoleWindow()
        {
            //conWin.Owner = this;
            conWin.Show();
        }
        private void networkTools(object sender, RoutedEventArgs e)
        {
            NetworkToolWindow nwT = new NetworkToolWindow();
            nwT.Owner = this;
            nwT.ShowDialog();
        }
        private void patchDayList(object sender, RoutedEventArgs e)
        {
            PatchDayWindow pdW = new PatchDayWindow();
            pdW.Owner = this;
            pdW.Show();
        }
        private void sysControl(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"control");
        }
        private void sysServices(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"services.msc");
        }
        private void progDriver(object sender, RoutedEventArgs e)
        {
            buildProg(@"IObit\Driver Booster\DriverBooster.exe", "http://update.iobit.com/dl/db7/driver_booster_setup_free.exe");
        }
        private void progTeamviewer(object sender, RoutedEventArgs e)
        {
            TeamviewerWindow teamviewerWindow = new TeamviewerWindow();
            teamviewerWindow.Owner = this;
            teamviewerWindow.Show();
        }
        private void progRegistry(object sender, RoutedEventArgs e)
        {
            buildProg(@"Wise\Wise Registry Cleaner\WiseRegCleaner.exe", "http://downloads.wisecleaner.com/soft/WRCFree.exe");
        }
        private void progDefrag(object sender, RoutedEventArgs e)
        {
            buildProg(@"Auslogics\Disk Defrag\DiskDefrag.exe", "http://static.auslogics.com/en/disk-defrag/disk-defrag-setup.exe");
        }

        public void buildProg(String path, String url)
        {
            String tmp = @"C:\Program Files\" + path;
            if (!File.Exists(tmp))
            {
                tmp = @"C:\Program Files (x86)\" + path;
            }
            if (File.Exists(tmp))
            {
                System.Diagnostics.Process.Start(tmp);
            }
            else
            {
                MessageBox.Show("Die Software muss erst heruntergeladen und installiert werden. \n Sie werden weitergeleitet!", "Software fehlt.");
                System.Diagnostics.Process.Start(url);
            }
            
        }
        public void buildCMD(String command, String name, String afterMessage)
        {
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".cmd";
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("@echo off ");
                sw.WriteLine("color 02");
                sw.WriteLine("echo.");
                sw.WriteLine("echo.");
                sw.WriteLine("echo.");
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo Starte " + name);
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo.");
                sw.WriteLine(command);
                sw.WriteLine("echo.");
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo Abgeschlossen! " + afterMessage);
                sw.WriteLine("echo -----------------------------------");
            }
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = fileName;
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;
            cmdStartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += writeLog;
            cmdProcess.OutputDataReceived += writeLog;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();
        }
        public void runCMD(String command, String name, String afterMessage)
        {
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".cmd";
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("@echo off ");
                sw.WriteLine("echo.");
                sw.WriteLine("echo.");
                sw.WriteLine("echo.");
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo Starte " + name);
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo.");
                sw.WriteLine(command);
                sw.WriteLine("echo.");
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("echo Abgeschlossen! " + afterMessage);
                sw.WriteLine("echo -----------------------------------");
                sw.WriteLine("pause");
            }
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = fileName;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.Start();
        }
        public void writeLog(object sender, DataReceivedEventArgs e)
        {
            logs += "\n" + e.Data;
            this.Dispatcher.Invoke((Action)(() =>
            {
                conWin.refreshLog(e.Data);
            }));
        }
        private const int POWER_REQUEST_CONTEXT_VERSION = 0;
        private const int POWER_REQUEST_CONTEXT_SIMPLE_STRING = 0x1;
        private static IntPtr currentPowerRequest;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr PowerCreateRequest(ref POWER_REQUEST_CONTEXT Context);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool PowerSetRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool PowerClearRequest(IntPtr PowerRequestHandle, PowerRequestType RequestType);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct POWER_REQUEST_CONTEXT
        {
            public UInt32 Version;
            public UInt32 Flags;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string SimpleReasonString;
        }

        internal enum PowerRequestType
        {
            PowerRequestDisplayRequired, // Not to be used by drivers
            PowerRequestSystemRequired,
            PowerRequestAwayModeRequired, // Not to be used by drivers
            PowerRequestExecutionRequired // Not to be used by drivers
        }
        private static void SuppressStandby()
        {
            // Clear current power request if there is any.
            if (currentPowerRequest != IntPtr.Zero)
            {
                PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestSystemRequired);
                PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestExecutionRequired);
                PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestAwayModeRequired);
                PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestDisplayRequired);
                currentPowerRequest = IntPtr.Zero;
            }

            // Create new power request.
            POWER_REQUEST_CONTEXT pContext;
            pContext.Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING;
            pContext.Version = POWER_REQUEST_CONTEXT_VERSION;
            pContext.SimpleReasonString = "Standby suppressed by Windows Admin Toolbox.exe";

            currentPowerRequest = PowerCreateRequest(ref pContext);

            if (currentPowerRequest == IntPtr.Zero)
            {
                // Failed to create power request.
                var error = Marshal.GetLastWin32Error();

                if (error != 0)
                    throw new Win32Exception(error);
            }

            checkPowerState(PowerSetRequest(currentPowerRequest, PowerRequestType.PowerRequestSystemRequired));
            checkPowerState(PowerSetRequest(currentPowerRequest, PowerRequestType.PowerRequestAwayModeRequired));
            checkPowerState(PowerSetRequest(currentPowerRequest, PowerRequestType.PowerRequestDisplayRequired));
            checkPowerState(PowerSetRequest(currentPowerRequest, PowerRequestType.PowerRequestExecutionRequired));


        }
        private static bool checkPowerState(bool success)
        {
            if (!success)
            {
                // Failed to set power request.
                currentPowerRequest = IntPtr.Zero;
                var error = Marshal.GetLastWin32Error();

                if (error != 0)
                    throw new Win32Exception(error);
            }
            return success;
        }
        private static void EnableStandby()
        {
            // Only try to clear power request if any power request is set.
            if (currentPowerRequest != IntPtr.Zero)
            {
                
                if( checkPowerState(PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestSystemRequired))   || 
                    checkPowerState(PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestSystemRequired))   || 
                    checkPowerState(PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestAwayModeRequired)) ||
                    checkPowerState(PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestDisplayRequired))  ||
                    checkPowerState(PowerClearRequest(currentPowerRequest, PowerRequestType.PowerRequestExecutionRequired)))
                {
                    currentPowerRequest = IntPtr.Zero;
                }
            }
        }
    }
}
