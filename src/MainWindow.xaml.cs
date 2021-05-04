using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace JlinkDownloader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Binary files(*.bin)|*.bin",
            };
            if (dlg.ShowDialog(this) == true)
            {
                file.Text = dlg.FileName;
            }
        }

        private void OnDownloadClick(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            if (string.IsNullOrEmpty(file.Text))
            {
                MessageBox.Show("please select the file to download");
                return;
            }
            var sb = new StringBuilder(100);
            int ret = JLinkARM.JLINK_GetFirmwareString(sb, 100);
            Debug.Assert(ret == 0);
            if (ret != 0)
            {
                MessageBox.Show("can't get the firmware, please check the target is connected");
                return;
            }
            try
            {
                sb.AppendLine();

                ret = JLinkARM.JLINK_ExecCommand($"device={device.Text}");
                Debug.Assert(ret == 0);
                sb.AppendLine($"select device return: {ret}");
                ret = JLinkARM.JLINK_TIF_Select(JLinkARM.JLINK_TIF_SWD);
                Debug.Assert(ret == 0);
                sb.AppendLine($"select tif return: {ret}");
                ret = JLinkARM.JLINK_SetSpeed(int.Parse(speed.Text));
                Debug.Assert(ret == 0);
                sb.AppendLine($"set speed return: {ret}");
                ret = JLinkARM.JLINK_Connect();
                Debug.Assert(ret == 0);
                sb.AppendLine($"connect return: {ret}");
                ret = JLinkARM.JLINK_Reset();
                Debug.Assert(ret == 0);
                sb.AppendLine($"reset return: {ret}");
                ret = JLinkARM.JLINK_Halt();
                Debug.Assert(ret == 0);
                sb.AppendLine($"halt return: {ret}");
                ret = JLinkARM.JLINK_EraseChip();
                Debug.Assert(ret == 0);
                sb.AppendLine($"erase return: {ret}");
                ret = JLinkARM.JLINK_DownloadFile(file.Text, int.Parse(address.Text, System.Globalization.NumberStyles.HexNumber));
                Debug.Assert(ret == 0);
                sb.AppendLine($"download return: {ret}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                result.Text = sb.ToString();
            }
        }
    }
}
