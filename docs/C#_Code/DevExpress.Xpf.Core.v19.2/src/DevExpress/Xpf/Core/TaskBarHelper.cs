namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    internal class TaskBarHelper
    {
        private const int ABS_AUTOHIDE = 1;

        [SecurityCritical]
        public static bool IsAutoHide(out int edge, IntPtr handle)
        {
            edge = 0;
            APPBARDATA structure = new APPBARDATA();
            structure.cbSize = Marshal.SizeOf<APPBARDATA>(structure);
            structure.hWnd = handle;
            if ((SHAppBarMessage(4, ref structure) & 1) == 0)
            {
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                edge = i;
                structure.uEdge = (uint) i;
                if (SHAppBarMessage(7, ref structure) != 0)
                {
                    return true;
                }
            }
            edge = 3;
            return true;
        }

        [DllImport("SHELL32", CallingConvention=CallingConvention.StdCall)]
        private static extern int SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        internal enum ABEdge
        {
            ABE_LEFT,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }

        internal enum ABMsg
        {
            ABM_NEW,
            ABM_REMOVE,
            ABM_QUERYPOS,
            ABM_SETPOS,
            ABM_GETSTATE,
            ABM_GETTASKBARPOS,
            ABM_ACTIVATE,
            ABM_GETAUTOHIDEBAR,
            ABM_SETAUTOHIDEBAR,
            ABM_WINDOWPOSCHANGED,
            ABM_SETSTATE
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public DevExpress.Xpf.Core.NativeMethods.RECT rc;
            public IntPtr lParam;
        }
    }
}

