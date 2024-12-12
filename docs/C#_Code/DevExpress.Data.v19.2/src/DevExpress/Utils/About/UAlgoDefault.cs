namespace DevExpress.Utils.About
{
    using DevExpress.Internal;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class UAlgoDefault : UAlgo
    {
        private List<byte> list = new List<byte>();
        internal const int kProjectMax = 0x1000;
        private UAlgoPost postInfo;

        private void CheckFlush()
        {
            this.FlushList();
        }

        private byte CheckPlatform(byte platform)
        {
            platform ??= ((UAlgo.DefaultPlatform != null) ? UAlgo.DefaultPlatform.Value : base.LastPlatform);
            return platform;
        }

        private bool CheckPost()
        {
            if (AlgoProvider.Id == null)
            {
                return false;
            }
            if (this.PostInfo.disabled)
            {
                this.Disable();
                return false;
            }
            if (!File.Exists(this.Path))
            {
                return false;
            }
            FileInfo info = new FileInfo(this.Path);
            return (((info.Length <= 0L) || (DateTime.Now.Subtract(info.LastWriteTime).TotalHours <= 6.0)) ? (info.Length > 0x3e8L) : true);
        }

        private void CheckUpdateStatus(byte kind)
        {
            if (UAlgo.Status == null)
            {
                if (this.IsDemo(kind))
                {
                    UAlgo.Status = 1;
                }
                if ((kind == 10) || ((kind == 11) || ((kind == 12) || (kind == 13))))
                {
                    UAlgo.Status = new byte?(kind);
                }
                if ((kind == 90) || (kind == 0x5b))
                {
                    UAlgo.Status = new byte?(kind);
                }
            }
        }

        private void Disable()
        {
            UAlgo.Enabled = false;
            RegistryKey key = GetKey(true);
            if (key != null)
            {
                key.SetValue("CustomerExperienceProgram", "");
                key.Close();
            }
        }

        public override void DoCustomEvent(byte platform, string json)
        {
            this.DoEvent(110, platform, json);
        }

        public override void DoEvent(byte kind, string action)
        {
            this.DoEvent(kind, 0, action);
        }

        public override void DoEvent(byte kind, Type action)
        {
            this.DoEvent(kind, 0, action);
        }

        public override void DoEvent(byte kind, byte platform, string action)
        {
            this.DoEvent(kind, platform, action, CRC32.Default.ComputeHash(action));
        }

        public override void DoEvent(byte kind, byte platform, Type action)
        {
            try
            {
                this.DoEvent(kind, platform, action.FullName);
            }
            catch
            {
            }
        }

        internal virtual void DoEvent(byte kind, byte platform, string action, uint actionNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(action))
                {
                    this.CheckUpdateStatus(kind);
                    platform = this.CheckPlatform(platform);
                    base.LastPlatform = platform;
                    if (UAlgo.Enabled)
                    {
                        long num = DateTime.UtcNow.ToFileTime();
                        int count = this.list.Count;
                        this.list.Add(Utility.IsLic() ? ((byte) 1) : ((byte) 0));
                        this.list.AddRange(BitConverter.GetBytes(0));
                        this.list.AddRange(BitConverter.GetBytes(num));
                        this.list.Add(kind);
                        this.list.Add(platform);
                        this.list.AddRange(BitConverter.GetBytes(actionNumber));
                        byte[] bytes = Encoding.Default.GetBytes(action.Substring(0, Math.Min(action.Length, this.GetTextLength(kind))));
                        this.list.AddRange(BitConverter.GetBytes((short) bytes.Length));
                        this.list.AddRange(bytes);
                        this.CheckFlush();
                    }
                }
            }
            catch
            {
            }
        }

        public override void DoEventException(Exception e)
        {
            try
            {
                string text = new UAlgoExceptionReportGenerator().GetText(e);
                this.DoEvent(0x1f, base.LastPlatform, text);
            }
            catch
            {
            }
        }

        public override void DoEventException(UnhandledExceptionEventArgs e)
        {
            try
            {
                string text = new UAlgoExceptionReportGenerator().GetText(e.ExceptionObject as Exception);
                this.DoEvent(0x1f, base.LastPlatform, text);
            }
            catch
            {
            }
        }

        public override void DoEventInstall(byte kind)
        {
            UserData info = Utility.GetInfo();
            string str = ((info == null) || (info.UserNo < 0)) ? "Trial" : info.UserNo.ToString();
            this.DoEvent(kind, 1, (kind == 90) ? str : str);
        }

        public override void DoEventObject(byte kind, object instance)
        {
            this.DoEventObject(kind, 0, instance);
        }

        public override void DoEventObject(byte kind, byte platform, object instance)
        {
            if (instance != null)
            {
                try
                {
                    string fullName = instance.GetType().FullName;
                    if (this.IsDemo(kind) || fullName.ToLowerInvariant().StartsWith("devexpress."))
                    {
                        this.DoEvent(kind, platform, instance.GetType());
                    }
                }
                catch
                {
                }
            }
        }

        public override void DoEventProject(string json)
        {
            Project();
            if (UAlgo.Enabled)
            {
                this.DoEvent(40, 0, json);
            }
        }

        public override void DoEventTemplate(byte platform, object instance, object[] customParams)
        {
            try
            {
                string fullName = instance.GetType().FullName;
                if ((customParams != null) && (customParams.Length != 0))
                {
                    fullName = System.IO.Path.GetFileName(customParams[0].ToString());
                }
                this.DoEvent(13, platform, fullName);
            }
            catch
            {
            }
        }

        private void DoPost(int wait)
        {
            if ((UAlgo.Enabled && (this.Path != null)) && this.PostInfo.Post())
            {
                DateTime now = DateTime.Now;
                while (this.PostInfo.IsWorking)
                {
                    Thread.Sleep(100);
                    TimeSpan span = now.Subtract(DateTime.Now);
                    if (span.TotalMilliseconds > wait)
                    {
                        break;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public override void DoStackEvent(byte platform)
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            this.DoEvent(20, platform, $"{method.DeclaringType.FullName}.{method.Name}");
        }

        private void FlushList()
        {
            if ((this.list != null) && (this.list.Count != 0))
            {
                try
                {
                    if (UAlgo.Enabled)
                    {
                        if (this.Path != null)
                        {
                            bool flag = this.CheckPost();
                            using (FileStream stream = new FileStream(this.Path, FileMode.Append, FileAccess.Write))
                            {
                                byte[] buffer = this.list.ToArray();
                                stream.Write(buffer, 0, buffer.Length);
                            }
                            if (flag)
                            {
                                this.PostInfo.Post();
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                }
                this.list.Clear();
            }
        }

        private int GetTextLength(byte kind) => 
            ((kind == 30) || ((kind == 0x21) || ((kind == 0x20) || (kind == 0x1f)))) ? 0x990 : ((kind != 40) ? 160 : 0x1000);

        private bool IsDemo(byte kind) => 
            (kind == 1) || ((kind == 3) || ((kind == 2) || (kind == 4)));

        public string Path =>
            UAlgo.PathInfo.Path;

        private UAlgoPost PostInfo
        {
            get
            {
                this.postInfo ??= new UAlgoPost(this.Path, AlgoProvider.Id.Value, UAlgo.VersionId);
                return this.postInfo;
            }
        }
    }
}

