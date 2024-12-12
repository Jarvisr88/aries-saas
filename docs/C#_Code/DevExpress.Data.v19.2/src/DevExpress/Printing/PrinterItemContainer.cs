namespace DevExpress.Printing
{
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Printing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public class PrinterItemContainer
    {
        private PrinterObtainMode obtainMode;
        private List<PrinterItem> items;
        private string defaultPrinterName;

        public PrinterItemContainer() : this(PrinterObtainMode.LocalPrintServer)
        {
        }

        public PrinterItemContainer(PrinterObtainMode obtainMode)
        {
            this.obtainMode = obtainMode;
        }

        [DllImport("winspool.drv", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern int EnumPrinters(int flags, string name, int level, IntPtr pPrinterEnum, int cbBuf, out int pcbNeeded, out int pcReturned);
        [DllImport("winspool.drv", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);
        private string GetDefaultPrinterName() => 
            (this.obtainMode == PrinterObtainMode.LocalPrintServer) ? this.GetDefaultPrinterNameUsingServer() : this.GetDefaultPrinterNameUsingWinApi();

        private string GetDefaultPrinterNameUsingServer()
        {
            string fullName = string.Empty;
            LocalPrintServer server = new LocalPrintServer();
            try
            {
                fullName = server.DefaultPrintQueue.FullName;
            }
            catch
            {
            }
            finally
            {
                if (server != null)
                {
                    server.Dispose();
                }
            }
            return fullName;
        }

        [SecuritySafeCritical]
        private string GetDefaultPrinterNameUsingWinApi()
        {
            string str = string.Empty;
            StringBuilder pszBuffer = new StringBuilder(0x100);
            int capacity = pszBuffer.Capacity;
            try
            {
                if (GetDefaultPrinter(pszBuffer, ref capacity))
                {
                    str = pszBuffer.ToString(0, capacity - 1);
                }
            }
            catch
            {
            }
            return str;
        }

        private List<string> GetFullNamesOfSpecifiedTypes(LocalPrintServer printServer, EnumeratedPrintQueueTypes[] enumerationFlag)
        {
            using (PrintQueueCollection queues = printServer.GetPrintQueues(enumerationFlag))
            {
                Func<PrintQueue, string> selector = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<PrintQueue, string> local1 = <>c.<>9__14_0;
                    selector = <>c.<>9__14_0 = item => item.FullName;
                }
                return queues.Select<PrintQueue, string>(selector).ToList<string>();
            }
        }

        private string GetPrinterDisplayName(PRINTER_INFO_2 pi)
        {
            if (!((PrinterAttributes) pi.Attributes).HasFlag(PrinterAttributes.PRINTER_ATTRIBUTE_NETWORK) || (pi.pPrinterName.IndexOf(pi.pServerName) != 0))
            {
                return pi.pPrinterName;
            }
            string str2 = pi.pPrinterName.Substring(pi.pServerName.Length).Replace(@"\", string.Empty);
            return string.Format(PreviewStringId.NetworkPrinterFormat.GetString(), str2, pi.pServerName.Replace(@"\", string.Empty));
        }

        [SecuritySafeCritical]
        private List<PRINTER_INFO_2> GetPrinters()
        {
            List<PRINTER_INFO_2> list2;
            int level = 2;
            int num2 = (IntPtr.Size * 13) + (Marshal.SizeOf(typeof(int)) * 8);
            List<PRINTER_INFO_2> list = new List<PRINTER_INFO_2>();
            try
            {
                int num3;
                int num4;
                EnumPrinters(6, null, level, IntPtr.Zero, 0, out num3, out num4);
                IntPtr pPrinterEnum = Marshal.AllocCoTaskMem(num3);
                if (EnumPrinters(6, null, level, pPrinterEnum, num3, out num3, out num4) != 0)
                {
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= num4)
                        {
                            Marshal.FreeCoTaskMem(pPrinterEnum);
                            break;
                        }
                        IntPtr ptr2 = (IntPtr) (((long) pPrinterEnum) + (num6 * num2));
                        PRINTER_INFO_2 item = (PRINTER_INFO_2) Marshal.PtrToStructure(ptr2, typeof(PRINTER_INFO_2));
                        list.Add(item);
                        num6++;
                    }
                    return list;
                }
                else
                {
                    Marshal.FreeCoTaskMem(pPrinterEnum);
                    list2 = null;
                }
            }
            catch
            {
                list2 = null;
            }
            return list2;
        }

        private void LoadItems()
        {
            PrinterObtainMode obtainMode = this.obtainMode;
            if (obtainMode == PrinterObtainMode.LocalPrintServer)
            {
                this.LoadItemsUsingServer();
            }
            else if (obtainMode == PrinterObtainMode.WinApi)
            {
                this.LoadItemsUsingWinApi();
            }
            this.SortItems();
        }

        private void LoadItemsUsingServer()
        {
            using (LocalPrintServer server = new LocalPrintServer())
            {
                EnumeratedPrintQueueTypes[] enumerationFlag = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.WorkOffline, EnumeratedPrintQueueTypes.Connections };
                List<string> fullNamesOfSpecifiedTypes = this.GetFullNamesOfSpecifiedTypes(server, enumerationFlag);
                EnumeratedPrintQueueTypes[] typesArray2 = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Fax, EnumeratedPrintQueueTypes.Connections };
                List<string> list2 = this.GetFullNamesOfSpecifiedTypes(server, typesArray2);
                EnumeratedPrintQueueTypes[] typesArray3 = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Local };
                using (PrintQueueCollection queues = server.GetPrintQueues(typesArray3))
                {
                    foreach (PrintQueue queue in queues)
                    {
                        this.items.Add(new PrinterItem(queue, list2.Contains(queue.FullName), false, Equals(queue.FullName, this.DefaultPrinterName), fullNamesOfSpecifiedTypes.Contains(queue.FullName)));
                    }
                }
                EnumeratedPrintQueueTypes[] typesArray4 = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Connections };
                using (PrintQueueCollection queues2 = server.GetPrintQueues(typesArray4))
                {
                    foreach (PrintQueue queue2 in queues2)
                    {
                        this.items.Add(new PrinterItem(queue2, list2.Contains(queue2.FullName), true, Equals(queue2.FullName, this.DefaultPrinterName), fullNamesOfSpecifiedTypes.Contains(queue2.FullName)));
                    }
                }
            }
        }

        private void LoadItemsUsingWinApi()
        {
            foreach (PRINTER_INFO_2 printer_info_ in this.GetPrinters())
            {
                uint cJobs = printer_info_.cJobs;
                PrinterItem item = new PrinterItem(printer_info_.pPrinterName, this.GetPrinterDisplayName(printer_info_), printer_info_.pLocation, printer_info_.pComment, cJobs.ToString(), (PrinterStatus) printer_info_.Status);
                PrinterAttributes attributes = (PrinterAttributes) printer_info_.Attributes;
                item.InitPrinterTypeFlags(attributes.HasFlag(PrinterAttributes.PRINTER_ATTRIBUTE_FAX), attributes.HasFlag(PrinterAttributes.PRINTER_ATTRIBUTE_NETWORK), Equals(printer_info_.pPrinterName, this.DefaultPrinterName), attributes.HasFlag(PrinterAttributes.PRINTER_ATTRIBUTE_WORK_OFFLINE));
                this.items.Add(item);
            }
        }

        private void SortItems()
        {
            Comparison<PrinterItem> comparison = <>c.<>9__11_0 ??= (item1, item2) => Comparer<string>.Default.Compare(item1.DisplayName, item2.DisplayName);
            this.items.Sort(comparison);
        }

        public string DefaultPrinterName
        {
            get
            {
                this.defaultPrinterName ??= this.GetDefaultPrinterName();
                return this.defaultPrinterName;
            }
        }

        public IList<PrinterItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new List<PrinterItem>();
                    this.LoadItems();
                }
                return this.items;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrinterItemContainer.<>c <>9 = new PrinterItemContainer.<>c();
            public static Comparison<PrinterItem> <>9__11_0;
            public static Func<PrintQueue, string> <>9__14_0;

            internal string <GetFullNamesOfSpecifiedTypes>b__14_0(PrintQueue item) => 
                item.FullName;

            internal int <SortItems>b__11_0(PrinterItem item1, PrinterItem item2) => 
                Comparer<string>.Default.Compare(item1.DisplayName, item2.DisplayName);
        }
    }
}

