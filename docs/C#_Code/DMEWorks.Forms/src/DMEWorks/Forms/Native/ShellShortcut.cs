namespace DMEWorks.Forms.Native
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class ShellShortcut : IDisposable
    {
        private const int INFOTIPSIZE = 0x400;
        private const int MAX_PATH = 260;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWMINNOACTIVE = 7;
        private IShellLinkA m_Link;
        private string m_sPath;

        public ShellShortcut(string linkPath)
        {
            this.m_sPath = linkPath;
            this.m_Link = (IShellLinkA) new DMEWorks.Forms.Native.ShellLink();
            if (File.Exists(linkPath))
            {
                ((IPersistFile) this.m_Link).Load(linkPath, 0);
            }
        }

        public void Dispose()
        {
            if (this.m_Link != null)
            {
                Marshal.ReleaseComObject(this.m_Link);
                this.m_Link = null;
            }
        }

        public void Save()
        {
            ((IPersistFile) this.m_Link).Save(this.m_sPath, true);
        }

        public string Arguments
        {
            get
            {
                StringBuilder pszArgs = new StringBuilder(0x400);
                this.m_Link.GetArguments(pszArgs, pszArgs.Capacity);
                return pszArgs.ToString();
            }
            set => 
                this.m_Link.SetArguments(value);
        }

        public string Description
        {
            get
            {
                StringBuilder pszName = new StringBuilder(0x400);
                this.m_Link.GetDescription(pszName, pszName.Capacity);
                return pszName.ToString();
            }
            set => 
                this.m_Link.SetDescription(value);
        }

        public string WorkingDirectory
        {
            get
            {
                StringBuilder pszDir = new StringBuilder(260);
                this.m_Link.GetWorkingDirectory(pszDir, pszDir.Capacity);
                return pszDir.ToString();
            }
            set => 
                this.m_Link.SetWorkingDirectory(value);
        }

        public string Path
        {
            get
            {
                WIN32_FIND_DATAA pfd = new WIN32_FIND_DATAA();
                StringBuilder pszFile = new StringBuilder(260);
                this.m_Link.GetPath(pszFile, pszFile.Capacity, out pfd, SLGP_FLAGS.SLGP_UNCPRIORITY);
                return pszFile.ToString();
            }
            set => 
                this.m_Link.SetPath(value);
        }

        public string IconPath
        {
            get
            {
                int num;
                StringBuilder pszIconPath = new StringBuilder(260);
                this.m_Link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out num);
                return pszIconPath.ToString();
            }
            set => 
                this.m_Link.SetIconLocation(value, this.IconIndex);
        }

        public int IconIndex
        {
            get
            {
                int num;
                StringBuilder pszIconPath = new StringBuilder(260);
                this.m_Link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out num);
                return num;
            }
            set => 
                this.m_Link.SetIconLocation(this.IconPath, value);
        }

        public System.Drawing.Icon Icon
        {
            get
            {
                int num;
                System.Drawing.Icon icon2;
                StringBuilder pszIconPath = new StringBuilder(260);
                this.m_Link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out num);
                IntPtr handle = Native.ExtractIcon(Marshal.GetHINSTANCE(base.GetType().Module), pszIconPath.ToString(), num);
                if (handle == IntPtr.Zero)
                {
                    return null;
                }
                try
                {
                    using (System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(handle))
                    {
                        icon2 = (System.Drawing.Icon) icon.Clone();
                    }
                }
                finally
                {
                    Native.DestroyIcon(handle);
                }
                return icon2;
            }
        }

        public ProcessWindowStyle WindowStyle
        {
            get
            {
                int num;
                this.m_Link.GetShowCmd(out num);
                if (num != 2)
                {
                    if (num == 3)
                    {
                        return ProcessWindowStyle.Maximized;
                    }
                    if (num != 7)
                    {
                        return ProcessWindowStyle.Normal;
                    }
                }
                return ProcessWindowStyle.Minimized;
            }
            set
            {
                int num;
                switch (value)
                {
                    case ProcessWindowStyle.Normal:
                        num = 1;
                        break;

                    case ProcessWindowStyle.Minimized:
                        num = 7;
                        break;

                    case ProcessWindowStyle.Maximized:
                        num = 3;
                        break;

                    default:
                        throw new ArgumentException("Unsupported ProcessWindowStyle value.");
                }
                this.m_Link.SetShowCmd(num);
            }
        }

        public Keys Hotkey
        {
            get
            {
                short num;
                this.m_Link.GetHotkey(out num);
                return (((Keys) ((num & 0xff00) << 8)) | (((Keys) num) & (Keys.OemClear | Keys.LButton)));
            }
            set
            {
                if ((value & ~Keys.KeyCode) == Keys.None)
                {
                    throw new ArgumentException("Hotkey must include a modifier key.");
                }
                short wHotkey = (short) ((((int) (value & -65536)) >> 8) | (value & Keys.KeyCode));
                this.m_Link.SetHotkey(wHotkey);
            }
        }

        public object ShellLink =>
            this.m_Link;

        private class Native
        {
            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);
            [DllImport("shell32.dll", CharSet=CharSet.Auto)]
            public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        }
    }
}

