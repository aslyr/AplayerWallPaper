using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using APlayerHelperLib;
using Vanara.PInvoke;

namespace TestPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        APlayerHelper player;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int S_OK = 0x00000000;


        private void OnStateChangedFunction(int nOldState, int nNewState)
        {
            textBox1.Text += ("OnStateChangedFunction: " + " nOldState:" + nOldState + " nNewState:" + nNewState) +
                             Environment.NewLine;
        }

        private void OnOpenSuccessFunction()
        {
            textBox1.Text += ("OnOpenSuccessFunction") + Environment.NewLine;
        }

        private void OnSeekCompletedFunction(int anPosition)
        {
            textBox1.Text += ("OnSeekCompletedFunction: " + anPosition) + Environment.NewLine;
        }

        private void OnBufferFunction(int nPercent)
        {
            textBox1.Text += ("OnBufferFunction: " + nPercent) + Environment.NewLine;
        }

        private void OnVideoSizeChangedFunction()
        {
            textBox1.Text += ("OnVideoSizeChangedFunction") + Environment.NewLine;
        }

        private void OnDownloadCodecFunction(string strCodecPath)
        {
            textBox1.Text += ("OnDownloadCodecFunction: " + strCodecPath) + Environment.NewLine;
        }

        private void OnEventFunction(int nEventCode, int nEventParam)
        {
            textBox1.Text += ("OnEventFunction: nEventCode:" + nEventCode + " nEventParam:" + nEventParam) +
                             Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file = "";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择要发送的文件";
            if (DialogResult.OK == fileDialog.ShowDialog())
            {
                //将选择的文件的全路径赋值给文本框
                file = fileDialog.FileName;
            }

            if (file == "") return;
            player.Open(file);

            //声音最大
            player.SetVolume(100);

            //设置循环播放
            player.SetConfig(119, "1");
            //设置图片背景透明
            player.SetConfig(608, "0");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var desktopHWND = GetWorkerW();
            player = new APlayerHelper(this.pictureBox1.Handle, 0, 0, this.pictureBox1.Width, this.pictureBox1.Height);
            //player.Create(this.Handle, 0, 0, 500, 300);
            player.OnMessage += OnMessageFunction;
            player.OnEvent += OnEventFunction;
            player.OnBuffer += OnBufferFunction;
            player.OnDownloadCodec += OnDownloadCodecFunction;
            player.OnOpenSuccess += OnOpenSuccessFunction;
            player.OnSeekCompleted += OnSeekCompletedFunction;
            player.OnStateChanged += OnStateChangedFunction;
            player.OnVideoSizeChanged += OnVideoSizeChangedFunction;
        }

        public void OnMessageFunction(int nMessage, int wParam, int lParam)
        {
            switch (nMessage)
            {
                case WM_LBUTTONDOWN:
                    textBox1.Text += "点击" + Environment.NewLine;
                    break;
                case (WM_LBUTTONUP):
                {
                    textBox1.Text += "松开" + Environment.NewLine;
                }
                    break;
                case (WM_LBUTTONDBLCLK):
                {
                    textBox1.Text += "双击" + Environment.NewLine;
                }
                    break;
            }

            //textBox1.Text += ("OnMessageFunction") + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
                var width = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width);
                var height = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height);
                var desktopHWND = GetWorkerW();
                User32.SetParent(player.GetWindow(), desktopHWND);
                textBox1.AppendText(width + $"   {height} 桌面大小\r\n");
                User32.SetWindowPos(player.GetWindow(), IntPtr.Zero, 0, 0, 1980,
                    1080, (User32.SetWindowPosFlags)(0));
               
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }


        //只用在播放时才能获取
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "版本：" + player.GetVersion() + Environment.NewLine;
            textBox1.Text += "119配置结果返回：" + player.GetConfig(119) + Environment.NewLine;
            textBox1.Text += "608配置结果返回：" + player.GetConfig(608) + Environment.NewLine;
            textBox1.Text += "获取url:" + player.GetConfig(4) + Environment.NewLine;
            textBox1.Text += "视频尺寸：" + player.GetVideoWidth() + "*" + player.GetVideoHeight() + Environment.NewLine;
            textBox1.Text += "Position:" + (player.GetPosition()).ToString() + Environment.NewLine;
            textBox1.Text += "GetBufferProgress:" + (player.GetBufferProgress()).ToString() + Environment.NewLine;
            textBox1.Text += "GetDuration:" + (player.GetDuration()).ToString() + Environment.NewLine;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            player.SetVolume(50);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            player.SetPosition(player.GetDuration() / 2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            player.Destroy();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var slist = player.GetConfig(10);
            var tietu = player.GetConfig(601);
            player.SetConfig(602, "1");
            player.SetConfig(2401, null);
            textBox1.AppendText(slist + "\r\n");
            textBox1.AppendText(tietu + "   贴图是否可用\r\n");
            player.SetConfig(606, "200");
            player.SetConfig(607, "100");
            player.SetConfig(612, "看看，的房价肯定");
            player.SetConfig(1801, "1");
            ////开启dlna投屏
            //var dlnalist = player.GetConfig(1802);
            //var xiaomis = dlnalist.Split(';');
            //if (xiaomis.Length>1)
            //{
            //    var xiaomi = xiaomis[1];
            //    textBox1.AppendText(xiaomi + "  dlna设备\r\n");
            //    player.SetConfig(1803, xiaomi);
            //    var dlna = player.GetConfig(1803);
            //    textBox1.AppendText(dlna + "  投射成功\r\n");
            //}
            var subCan = player.GetConfig(501);
            textBox1.AppendText(subCan + "  字幕是否可用\r\n");
            player.SetConfig(503, @"J:\bilibili视频\【晓丹】师妹今日下山，一起闯荡江湖？\下山.ass");
            textBox1.AppendText(player.GetConfig(503) + "  字幕地址\r\n");
            var sublist = player.GetConfig(505);
            textBox1.AppendText(sublist + "  字幕列表\r\n");
            var sublist2 = player.GetConfig(2105);
            textBox1.AppendText(player.GetConfig(504) + "  字幕是否显示\r\n");
            textBox1.AppendText(player.GetConfig(506) + "  字幕显示索引\r\n");
            //player.SetConfig(204, "1920;1080");
            //player.SetConfig(207, "1");
            player.SetConfig(506, "0");
            player.SetConfig(2103, @"J:\bilibili视频\【晓丹】师妹今日下山，一起闯荡江湖？\下山.ass");
            var desktopHWND=GetWorkerW();
            textBox1.AppendText(desktopHWND.DangerousGetHandle() + "  Progamn\r\n");
            
            User32.SetParent(player.GetWindow(), desktopHWND);
            
            int dpiX, dpiY;
            SystemDpi(out dpiX ,out dpiY);
            textBox1.AppendText(dpiX + $"   {dpiY} 桌面dpi\r\n");
            var scale = Scaling(dpiY);
            var width = Screen.PrimaryScreen.WorkingArea.Width;
            var height = Screen.PrimaryScreen.WorkingArea.Height;
            textBox1.AppendText(width + $"   {height} 桌面大小\r\n");
            User32.SetWindowPos(player.GetWindow(), IntPtr.Zero, 0, 0, width,
                height, (User32.SetWindowPosFlags)(0));
            
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //if (player != null)
            //{
            //    textBox1.AppendText(pictureBox1.Width + "\r\n");
            //    textBox1.AppendText(pictureBox1.Height + "\r\n");
            //    textBox1.AppendText(player.Handle + "  player句柄\r\n");
            //    var b=  User32.SetWindowPos(player.GetWindow(), new HWND(IntPtr.Zero),
            //        0, 0, pictureBox1.Width, pictureBox1.Height, (User32.SetWindowPosFlags)(0));
            //    textBox1.AppendText(b + "  设置大小\r\n");
            //}
        }

        private HWND GetWorkerW()
        {
            IntPtr result=IntPtr.Zero;
            var windowHandle = User32.FindWindow("Progman", null);
            User32.SendMessageTimeout(windowHandle, 0x052c, IntPtr.Zero, IntPtr.Zero, User32.SMTO.SMTO_NORMAL, 0x3e8, ref  result);
            User32.EnumWindows(enumWindow, IntPtr.Zero);
            User32.ShowWindow(_workerw, ShowWindowCommand.SW_HIDE);
            return windowHandle;
        }
        public HWND _workerw=HWND.NULL;
        private bool enumWindow(HWND hwnd, IntPtr lParam)
        {
            var defview = User32.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null);
            if (defview!=HWND.NULL)
            {
                _workerw = User32.FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", "0");
            }

            return true;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            User32.SetParent(player.GetWindow(), this.pictureBox1.Handle);
            User32.SetWindowPos(player.GetWindow(), IntPtr.Zero, 0, 0, pictureBox1.Width, pictureBox1.Height,
                (User32.SetWindowPosFlags)(0));
        }
        private void SystemDpi(out int x, out int y)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                x = (int)g.DpiX;
                y = (int)g.DpiY;
                g.Dispose();
            }
        }

        ///根据当前系统dpi数值匹配 当前系统的桌面缩放比例
        private double Scaling(int DpiIndex)
        {
            switch (DpiIndex)
            {
                case 96: return 1;
                case 120: return 1.25;
                case 144: return 1.5;
                case 168: return 1.75;
                case 192: return 2;
            }
            return 1;
        }
    }
}