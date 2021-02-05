using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace APlayerHelperLib
{
    // Token: 0x02000002 RID: 2
    public class APlayerHelper
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public APlayerHelper()
        {
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
        public APlayerHelper(IntPtr hParentWnd, int x, int y, int nWidth, int nHeight)
        {
            this.Handle = this.Create(hParentWnd, x, y, nWidth, nHeight);
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
        public IntPtr Create(IntPtr hParentWnd, int x, int y, int nWidth, int nHeight)
        {
            this.onMessage = new APlayerHelper.OnMessageDelegate(this.OnMessageFunction);
            this.onStateChanged = new APlayerHelper.OnStateChangedDelegate(this.OnStateChangedFunction);
            this.onOpenSuccess = new APlayerHelper.OnOpenSuccessDelegate(this.OnOpenSuccessFunction);
            this.onSeekCompleted = new APlayerHelper.OnSeekCompletedDelegate(this.OnSeekCompletedFunction);
            this.onBuffer = new APlayerHelper.OnBufferDelegate(this.OnBufferFunction);
            this.onVideoSizeChanged = new APlayerHelper.OnVideoSizeChangedDelegate(this.OnVideoSizeChangedFunction);
            this.onDownloadCodec = new APlayerHelper.OnDownloadCodecDelegate(this.OnDownloadCodecFunction);
            this.onEvent = new APlayerHelper.OnEventDelegate(this.OnEventFunction);
            this.Handle = APlayerHelper.APlayer_Create(hParentWnd, x, y, nWidth, nHeight, this.onMessage,
                this.onStateChanged, this.onOpenSuccess, this.onSeekCompleted, this.onBuffer, this.onVideoSizeChanged,
                this.onDownloadCodec, this.onEvent);
            return this.Handle;
        }

        // Token: 0x06000004 RID: 4
        [DllImport("APlayerCaller.dll")]
        private static extern IntPtr APlayer_Create(IntPtr hParentWnd, int x, int y, int nWidth, int nHeight,
            APlayerHelper.OnMessageDelegate OnMessage, APlayerHelper.OnStateChangedDelegate OnStateChanged,
            APlayerHelper.OnOpenSuccessDelegate OnOpenSuccess, APlayerHelper.OnSeekCompletedDelegate OnSeekCompleted,
            APlayerHelper.OnBufferDelegate OnBuffer, APlayerHelper.OnVideoSizeChangedDelegate OnVideoSizeChanged,
            APlayerHelper.OnDownloadCodecDelegate OnDownloadCodec, APlayerHelper.OnEventDelegate OnEvent);

        // Token: 0x06000005 RID: 5
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_Destroy(IntPtr aplayer);

        // Token: 0x06000006 RID: 6 RVA: 0x00002190 File Offset: 0x00000390
        public bool Destroy()
        {
            return APlayerHelper.APlayer_Destroy(this.Handle);
        }

        // Token: 0x06000007 RID: 7
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_OpenA(IntPtr aplayer, string pszUrl);

        // Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
        public bool Open(string pszUrl)
        {
            this.GetConfig(119);
            return APlayerHelper.APlayer_OpenA(this.Handle, pszUrl);
        }

        // Token: 0x06000009 RID: 9
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_Close(IntPtr aplayer);

        // Token: 0x0600000A RID: 10 RVA: 0x000021D8 File Offset: 0x000003D8
        public bool Close()
        {
            return APlayerHelper.APlayer_Close(this.Handle);
        }

        // Token: 0x0600000B RID: 11
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_Play(IntPtr aplayer);

        // Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
        public bool Play()
        {
            return APlayerHelper.APlayer_Play(this.Handle);
        }

        // Token: 0x0600000D RID: 13
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_Pause(IntPtr aplayer);

        // Token: 0x0600000E RID: 14 RVA: 0x00002218 File Offset: 0x00000418
        public bool Pause()
        {
            return APlayerHelper.APlayer_Pause(this.Handle);
        }

        // Token: 0x0600000F RID: 15
        [DllImport("APlayerCaller.dll")]
        private static extern string APlayer_GetVersion(IntPtr aplayer);

        // Token: 0x06000010 RID: 16 RVA: 0x00002238 File Offset: 0x00000438
        public string GetVersion()
        {
            return APlayerHelper.APlayer_GetVersion(this.Handle);
        }

        // Token: 0x06000011 RID: 17
        [DllImport("APlayerCaller.dll")]
        private static extern bool APlayer_SetCustomLogo(IntPtr aplayer, int nLogo);

        // Token: 0x06000012 RID: 18 RVA: 0x00002258 File Offset: 0x00000458
        public bool SetCustomLogo(IntPtr aplayer, int nLogo)
        {
            return APlayerHelper.APlayer_SetCustomLogo(this.Handle, nLogo);
        }

        // Token: 0x06000013 RID: 19
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetState(IntPtr aplayer);

        // Token: 0x06000014 RID: 20 RVA: 0x00002278 File Offset: 0x00000478
        public int GetState()
        {
            return APlayerHelper.APlayer_GetState(this.Handle);
        }

        // Token: 0x06000015 RID: 21
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetDuration(IntPtr aplayer);

        // Token: 0x06000016 RID: 22 RVA: 0x00002298 File Offset: 0x00000498
        public int GetDuration()
        {
            return APlayerHelper.APlayer_GetDuration(this.Handle);
        }

        // Token: 0x06000017 RID: 23
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetPosition(IntPtr aplayer);

        // Token: 0x06000018 RID: 24 RVA: 0x000022B8 File Offset: 0x000004B8
        public int GetPosition()
        {
            return APlayerHelper.APlayer_GetPosition(this.Handle);
        }

        // Token: 0x06000019 RID: 25
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_SetPosition(IntPtr aplayer, int nPosition);

        // Token: 0x0600001A RID: 26 RVA: 0x000022D8 File Offset: 0x000004D8
        public int SetPosition(int nPosition)
        {
            return APlayerHelper.APlayer_SetPosition(this.Handle, nPosition);
        }

        // Token: 0x0600001B RID: 27
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetVideoWidth(IntPtr aplayer);

        // Token: 0x0600001C RID: 28 RVA: 0x000022F8 File Offset: 0x000004F8
        public int GetVideoWidth()
        {
            return APlayerHelper.APlayer_GetVideoWidth(this.Handle);
        }

        // Token: 0x0600001D RID: 29
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetVideoHeight(IntPtr aplayer);

        // Token: 0x0600001E RID: 30 RVA: 0x00002318 File Offset: 0x00000518
        public int GetVideoHeight()
        {
            return APlayerHelper.APlayer_GetVideoHeight(this.Handle);
        }

        // Token: 0x0600001F RID: 31
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetVolume(IntPtr aplayer);

        // Token: 0x06000020 RID: 32 RVA: 0x00002338 File Offset: 0x00000538
        public int GetVolume()
        {
            return APlayerHelper.APlayer_GetVolume(this.Handle);
        }

        // Token: 0x06000021 RID: 33
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_SetVolume(IntPtr aplayer, int nVolume);

        // Token: 0x06000022 RID: 34 RVA: 0x00002358 File Offset: 0x00000558
        public int SetVolume(int nVolume)
        {
            return APlayerHelper.APlayer_SetVolume(this.Handle, nVolume);
        }

        // Token: 0x06000023 RID: 35
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_IsSeeking(IntPtr aplayer);

        // Token: 0x06000024 RID: 36 RVA: 0x00002378 File Offset: 0x00000578
        public int IsSeeking()
        {
            return APlayerHelper.APlayer_IsSeeking(this.Handle);
        }

        // Token: 0x06000025 RID: 37
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_GetBufferProgress(IntPtr aplayer);

        // Token: 0x06000026 RID: 38 RVA: 0x00002398 File Offset: 0x00000598
        public int GetBufferProgress()
        {
            return APlayerHelper.APlayer_GetBufferProgress(this.Handle);
        }

        // Token: 0x06000027 RID: 39
        [DllImport("APlayerCaller.dll")]
        private unsafe static extern string APlayer_GetConfigA(IntPtr aplayer, int nConfigId);

        // Token: 0x06000028 RID: 40 RVA: 0x000023B8 File Offset: 0x000005B8
        public string GetConfig(int nConfigId)
        {
            unsafe
            {
                 var  s = APlayer_GetConfigA(Handle, nConfigId);
                 return s;
            }
        }

        // Token: 0x06000029 RID: 41
        [DllImport("APlayerCaller.dll")]
        private static extern int APlayer_SetConfigA(IntPtr aplayer, int nConfigId, string strValue);

        // Token: 0x0600002A RID: 42 RVA: 0x000023D8 File Offset: 0x000005D8
        public int SetConfig(int nConfigId, string strValue)
        {
            return APlayerHelper.APlayer_SetConfigA(this.Handle, nConfigId, strValue);
        }

        // Token: 0x0600002B RID: 43
        [DllImport("APlayerCaller.dll")]
        private static extern IntPtr APlayer_GetWindow(IntPtr aplayer);

        // Token: 0x0600002C RID: 44 RVA: 0x000023F8 File Offset: 0x000005F8
        public IntPtr GetWindow()
        {
            return APlayerHelper.APlayer_GetWindow(this.Handle);
        }

        // Token: 0x14000001 RID: 1
        // (add) Token: 0x0600002D RID: 45 RVA: 0x00002418 File Offset: 0x00000618
        // (remove) Token: 0x0600002E RID: 46 RVA: 0x00002450 File Offset: 0x00000650

        public event APlayerHelper.OnMessageDelegate OnMessage;

        // Token: 0x14000002 RID: 2
        // (add) Token: 0x0600002F RID: 47 RVA: 0x00002488 File Offset: 0x00000688
        // (remove) Token: 0x06000030 RID: 48 RVA: 0x000024C0 File Offset: 0x000006C0

        public event APlayerHelper.OnStateChangedDelegate OnStateChanged;

        // Token: 0x14000003 RID: 3
        // (add) Token: 0x06000031 RID: 49 RVA: 0x000024F8 File Offset: 0x000006F8
        // (remove) Token: 0x06000032 RID: 50 RVA: 0x00002530 File Offset: 0x00000730

        public event APlayerHelper.OnOpenSuccessDelegate OnOpenSuccess;

        // Token: 0x14000004 RID: 4
        // (add) Token: 0x06000033 RID: 51 RVA: 0x00002568 File Offset: 0x00000768
        // (remove) Token: 0x06000034 RID: 52 RVA: 0x000025A0 File Offset: 0x000007A0

        public event APlayerHelper.OnSeekCompletedDelegate OnSeekCompleted;

        // Token: 0x14000005 RID: 5
        // (add) Token: 0x06000035 RID: 53 RVA: 0x000025D8 File Offset: 0x000007D8
        // (remove) Token: 0x06000036 RID: 54 RVA: 0x00002610 File Offset: 0x00000810

        public event APlayerHelper.OnBufferDelegate OnBuffer;

        // Token: 0x14000006 RID: 6
        // (add) Token: 0x06000037 RID: 55 RVA: 0x00002648 File Offset: 0x00000848
        // (remove) Token: 0x06000038 RID: 56 RVA: 0x00002680 File Offset: 0x00000880

        public event APlayerHelper.OnVideoSizeChangedDelegate OnVideoSizeChanged;

        // Token: 0x14000007 RID: 7
        // (add) Token: 0x06000039 RID: 57 RVA: 0x000026B8 File Offset: 0x000008B8
        // (remove) Token: 0x0600003A RID: 58 RVA: 0x000026F0 File Offset: 0x000008F0

        public event APlayerHelper.OnDownloadCodecDelegate OnDownloadCodec;

        // Token: 0x14000008 RID: 8
        // (add) Token: 0x0600003B RID: 59 RVA: 0x00002728 File Offset: 0x00000928
        // (remove) Token: 0x0600003C RID: 60 RVA: 0x00002760 File Offset: 0x00000960

        public event APlayerHelper.OnEventDelegate OnEvent;

        // Token: 0x0600003D RID: 61 RVA: 0x00002798 File Offset: 0x00000998
        private void OnMessageFunction(int nMessage, int wParam, int lParam)
        {
            bool flag = this.OnMessage != null;
            if (flag)
            {
                this.OnMessage(nMessage, wParam, lParam);
            }
        }

        // Token: 0x0600003E RID: 62 RVA: 0x000027C4 File Offset: 0x000009C4
        private void OnStateChangedFunction(int nOldState, int nNewState)
        {
            bool flag = this.OnStateChanged != null;
            if (flag)
            {
                this.OnStateChanged(nOldState, nNewState);
            }
        }

        // Token: 0x0600003F RID: 63 RVA: 0x000027F0 File Offset: 0x000009F0
        private void OnOpenSuccessFunction()
        {
            bool flag = this.OnOpenSuccess != null;
            if (flag)
            {
                this.OnOpenSuccess();
            }
        }

        // Token: 0x06000040 RID: 64 RVA: 0x0000281C File Offset: 0x00000A1C
        private void OnSeekCompletedFunction(int nPosition)
        {
            bool flag = this.OnSeekCompleted != null;
            if (flag)
            {
                this.OnSeekCompleted(nPosition);
            }
        }

        // Token: 0x06000041 RID: 65 RVA: 0x00002848 File Offset: 0x00000A48
        private void OnBufferFunction(int nPercent)
        {
            bool flag = this.OnBuffer != null;
            if (flag)
            {
                this.OnBuffer(nPercent);
            }
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00002874 File Offset: 0x00000A74
        private void OnVideoSizeChangedFunction()
        {
            bool flag = this.OnVideoSizeChanged != null;
            if (flag)
            {
                this.OnVideoSizeChanged();
            }
        }

        // Token: 0x06000043 RID: 67 RVA: 0x000028A0 File Offset: 0x00000AA0
        private void OnDownloadCodecFunction(string strCodecPath)
        {
            bool flag = this.OnDownloadCodec != null;
            if (flag)
            {
                this.OnDownloadCodec(strCodecPath);
            }
        }

        // Token: 0x06000044 RID: 68 RVA: 0x000028CC File Offset: 0x00000ACC
        private void OnEventFunction(int nEventCode, int nEventParam)
        {
            bool flag = this.OnEvent != null;
            if (flag)
            {
                this.OnEvent(nEventCode, nEventParam);
            }
        }

        // Token: 0x04000001 RID: 1
        public IntPtr Handle = IntPtr.Zero;

        // Token: 0x04000002 RID: 2

        // Token: 0x04000003 RID: 3
        private APlayerHelper.OnMessageDelegate onMessage;

        // Token: 0x04000004 RID: 4
        private APlayerHelper.OnStateChangedDelegate onStateChanged;

        // Token: 0x04000005 RID: 5
        private APlayerHelper.OnOpenSuccessDelegate onOpenSuccess;

        // Token: 0x04000006 RID: 6
        private APlayerHelper.OnSeekCompletedDelegate onSeekCompleted;

        // Token: 0x04000007 RID: 7
        private APlayerHelper.OnBufferDelegate onBuffer;

        // Token: 0x04000008 RID: 8
        private APlayerHelper.OnVideoSizeChangedDelegate onVideoSizeChanged;

        // Token: 0x04000009 RID: 9
        private APlayerHelper.OnDownloadCodecDelegate onDownloadCodec;

        // Token: 0x0400000A RID: 10
        private APlayerHelper.OnEventDelegate onEvent;

        // Token: 0x0400000B RID: 11
        private const int S_OK = 0;

        // Token: 0x02000003 RID: 3
        // (Invoke) Token: 0x06000046 RID: 70
        public delegate void OnMessageDelegate(int nMessage, int wParam, int lParam);

        // Token: 0x02000004 RID: 4
        // (Invoke) Token: 0x0600004A RID: 74
        public delegate void OnStateChangedDelegate(int nOldState, int nNewState);

        // Token: 0x02000005 RID: 5
        // (Invoke) Token: 0x0600004E RID: 78
        public delegate void OnOpenSuccessDelegate();

        // Token: 0x02000006 RID: 6
        // (Invoke) Token: 0x06000052 RID: 82
        public delegate void OnSeekCompletedDelegate(int nPosition);

        // Token: 0x02000007 RID: 7
        // (Invoke) Token: 0x06000056 RID: 86
        public delegate void OnBufferDelegate(int nPercent);

        // Token: 0x02000008 RID: 8
        // (Invoke) Token: 0x0600005A RID: 90
        public delegate void OnVideoSizeChangedDelegate();

        // Token: 0x02000009 RID: 9
        // (Invoke) Token: 0x0600005E RID: 94
        public delegate void OnDownloadCodecDelegate(string strCodecPath);

        // Token: 0x0200000A RID: 10
        // (Invoke) Token: 0x06000062 RID: 98
        public delegate void OnEventDelegate(int nEventCode, int nEventParam);
    }
}