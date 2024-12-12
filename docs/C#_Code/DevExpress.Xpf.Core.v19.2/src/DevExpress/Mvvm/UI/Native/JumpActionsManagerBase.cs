namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.ServiceModel;
    using System.Threading;

    public abstract class JumpActionsManagerBase : IDisposable
    {
        public const int DefaultMillisecondsTimeout = 500;
        private const string MainMutexName = "C4339FDC-8943-4AA6-8DB9-644D68462BC7_";
        protected const string InstancesFileName = "HB42A04D-FFA1-4755-9854-CB8DC81AAE89_";
        protected const string InstanceNamePrefix = "5BCE503C-DB8D-440A-BEEC-9C963A364DBF_";
        protected const string EndPointName = "Pipe";
        private Mutex mainMutex;
        private Tuple<IntPtr, IntPtr> instancesFile;
        private volatile bool disposed;
        private const byte MaxInstancesCount = 0x63;

        public JumpActionsManagerBase(int millisecondsTimeout)
        {
            this.MillisecondsTimeout = millisecondsTimeout;
        }

        private static byte CorceInstancesCount(int instancesCount) => 
            (byte) (instancesCount % 0x63);

        [SecurityCritical]
        protected Tuple<IntPtr, IntPtr> CreateFileMappingAndMapView(int dwMaximumSizeLow, string lpName, out bool alreadyExists)
        {
            dwMaximumSizeLow ??= 1;
            IntPtr hFileMappingObject = Import.CreateFileMapping(Import.InvalidHandleValue, IntPtr.Zero, 4, 0, (uint) dwMaximumSizeLow, lpName);
            if (hFileMappingObject == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            alreadyExists = Marshal.GetLastWin32Error() == 0xb7;
            IntPtr ptr2 = Import.MapViewOfFile(hFileMappingObject, 0xf001f, 0, 0, UIntPtr.Zero);
            if (ptr2 == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return new Tuple<IntPtr, IntPtr>(hFileMappingObject, ptr2);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                Mutex mutex = this.WaitMainMutex(!disposing);
                try
                {
                    this.DisposeInstancesFile();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            catch (TimeoutException)
            {
                if (disposing)
                {
                    throw;
                }
            }
        }

        [SecuritySafeCritical]
        private void DisposeInstancesFile()
        {
            if (this.instancesFile != null)
            {
                UnmapViewAndCloseFileMapping(this.instancesFile);
                this.instancesFile = null;
            }
        }

        ~JumpActionsManagerBase()
        {
            this.Dispose(false);
        }

        [SecuritySafeCritical]
        protected GuidData[] GetApplicationInstances(bool isCurrentProcessApplicationInstance)
        {
            Tuple<IntPtr, IntPtr> instancesFile = this.GetInstancesFile();
            int num = ((GuidData) Marshal.PtrToStructure(instancesFile.Item2, typeof(GuidData))).Byte0;
            GuidData[] dataArray = new GuidData[num];
            for (int i = 0; i < num; i++)
            {
                dataArray[i] = (GuidData) Marshal.PtrToStructure(instancesFile.Item2 + ((1 + i) * Marshal.SizeOf(typeof(GuidData))), typeof(GuidData));
            }
            return dataArray;
        }

        [SecuritySafeCritical]
        private Tuple<IntPtr, IntPtr> GetInstancesFile()
        {
            if (this.instancesFile == null)
            {
                bool flag;
                this.instancesFile = this.CreateFileMappingAndMapView(100 * Marshal.SizeOf(typeof(GuidData)), "HB42A04D-FFA1-4755-9854-CB8DC81AAE89_" + this.ApplicationId, out flag);
                if (!flag)
                {
                    try
                    {
                        this.UpdateInstancesFile(new GuidData[0]);
                    }
                    catch
                    {
                        this.DisposeInstancesFile();
                        throw;
                    }
                }
            }
            return this.instancesFile;
        }

        protected static string GetIsAliveFlagFileName(GuidData applicationInstance) => 
            $"IsAlive_{"5BCE503C-DB8D-440A-BEEC-9C963A364DBF_"}{applicationInstance.AsGuid}";

        [SecuritySafeCritical]
        protected Mutex GetMainMutex(bool safe)
        {
            if (safe)
            {
                return this.GetMainMutexCore(true);
            }
            this.mainMutex ??= this.GetMainMutexCore(false);
            return this.mainMutex;
        }

        [SecuritySafeCritical]
        protected Mutex GetMainMutexCore(bool safe) => 
            new Mutex(false, "C4339FDC-8943-4AA6-8DB9-644D68462BC7_" + this.ApplicationId);

        protected static string GetServiceUri(GuidData applicationInstance) => 
            $"net.pipe://localhost/{"5BCE503C-DB8D-440A-BEEC-9C963A364DBF_"}{applicationInstance.AsGuid}";

        [SecuritySafeCritical]
        protected bool IsAlive(GuidData instance)
        {
            bool alreadyExists = false;
            UnmapViewAndCloseFileMapping(this.CreateFileMappingAndMapView(1, GetIsAliveFlagFileName(instance), out alreadyExists));
            return alreadyExists;
        }

        [SecurityCritical]
        protected static void UnmapViewAndCloseFileMapping(Tuple<IntPtr, IntPtr> file)
        {
            Import.UnmapViewOfFile(file.Item2);
            Import.CloseHandle(file.Item1);
        }

        [SecuritySafeCritical]
        protected void UpdateInstancesFile(GuidData[] instances)
        {
            Tuple<IntPtr, IntPtr> instancesFile = this.GetInstancesFile();
            byte num = CorceInstancesCount(instances.Length);
            for (int i = 0; i < num; i++)
            {
                Marshal.StructureToPtr<GuidData>(instances[i], instancesFile.Item2 + ((1 + i) * Marshal.SizeOf(typeof(GuidData))), false);
            }
            GuidData structure = new GuidData {
                Byte0 = num
            };
            Marshal.StructureToPtr<GuidData>(structure, instancesFile.Item2, false);
        }

        protected Mutex WaitMainMutex(bool safe)
        {
            Mutex mainMutex = this.GetMainMutex(safe);
            try
            {
                this.WaitOne(mainMutex);
            }
            catch (AbandonedMutexException)
            {
            }
            return mainMutex;
        }

        protected void WaitOne(WaitHandle waitHandle)
        {
            if (!waitHandle.WaitOne(this.MillisecondsTimeout))
            {
                throw new TimeoutException();
            }
        }

        protected int MillisecondsTimeout { get; private set; }

        protected abstract string ApplicationId { get; }

        [StructLayout(LayoutKind.Sequential)]
        protected struct GuidData
        {
            public byte Byte0;
            public byte Byte1;
            public byte Byte2;
            public byte Byte3;
            public byte Byte4;
            public byte Byte5;
            public byte Byte6;
            public byte Byte7;
            public byte Byte8;
            public byte Byte9;
            public byte ByteA;
            public byte ByteB;
            public byte ByteC;
            public byte ByteD;
            public byte ByteE;
            public byte ByteF;
            public GuidData(Guid guid) : this(guid.ToByteArray())
            {
            }

            public GuidData(byte[] bytes)
            {
                this.Byte0 = bytes[0];
                this.Byte1 = bytes[1];
                this.Byte2 = bytes[2];
                this.Byte3 = bytes[3];
                this.Byte4 = bytes[4];
                this.Byte5 = bytes[5];
                this.Byte6 = bytes[6];
                this.Byte7 = bytes[7];
                this.Byte8 = bytes[8];
                this.Byte9 = bytes[9];
                this.ByteA = bytes[10];
                this.ByteB = bytes[11];
                this.ByteC = bytes[12];
                this.ByteD = bytes[13];
                this.ByteE = bytes[14];
                this.ByteF = bytes[15];
            }

            public byte[] AsBytesArray
            {
                get
                {
                    byte[] buffer1 = new byte[0x10];
                    buffer1[0] = this.Byte0;
                    buffer1[1] = this.Byte1;
                    buffer1[2] = this.Byte2;
                    buffer1[3] = this.Byte3;
                    buffer1[4] = this.Byte4;
                    buffer1[5] = this.Byte5;
                    buffer1[6] = this.Byte6;
                    buffer1[7] = this.Byte7;
                    buffer1[8] = this.Byte8;
                    buffer1[9] = this.Byte9;
                    buffer1[10] = this.ByteA;
                    buffer1[11] = this.ByteB;
                    buffer1[12] = this.ByteC;
                    buffer1[13] = this.ByteD;
                    buffer1[14] = this.ByteE;
                    buffer1[15] = this.ByteF;
                    return buffer1;
                }
            }
            public Guid AsGuid =>
                new Guid(this.AsBytesArray);
        }

        [ServiceContract]
        protected interface IApplicationInstance
        {
            [OperationContract]
            void Execute(string command);
        }

        private static class Import
        {
            public const uint FileMapAllAccess = 0xf001f;
            public const uint PageReadwrite = 4;
            public static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
            public const int ERROR_ALREADY_EXISTS = 0xb7;

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", SetLastError=true)]
            public static extern bool CloseHandle(IntPtr hObject);
            [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
            public static extern IntPtr CreateFileMapping(IntPtr lpBaseAddress, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);
            [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
            public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumberOfBytesToMap);
            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
            public static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
        }
    }
}

