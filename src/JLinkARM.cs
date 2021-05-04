using System.Runtime.InteropServices;
using System.Text;

namespace JlinkDownloader
{
    public class JLinkARM
    {
        public const string DllName = "JLinkARM.dll";
        public const int JLINK_TIF_JTAG = 0;
        public const int JLINK_TIF_SWD = 1;
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_GetFirmwareString([MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)] StringBuilder sb, int len);
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_ExecCommand(string cmd, int par1 = 0, int par2 = 0);
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_TIF_Select(int type);
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_SetSpeed(int speed);
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern bool JLINK_IsConnected();
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_Connect();
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_Reset();
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_Halt();
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_EraseChip();
        [DllImport(DllName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int JLINK_DownloadFile(string fileName, int addr);
    }
}
