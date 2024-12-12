namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data.Utils;
    using DevExpress.Export;
    using System;
    using System.Reflection;
    using System.Windows.Forms;

    public static class ClipboardHelper<TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        private static Assembly printingAssembly;

        static ClipboardHelper()
        {
            ClipboardHelper<TCol, TRow>.printingAssembly = AssemblyCache.LoadDXAssembly("DevExpress.XtraPrinting.v19.2");
        }

        public static IClipboardManager<TCol, TRow> GetManager(IClipboardGridView<TCol, TRow> gridView, ClipboardOptions exportOptions)
        {
            System.Type type = ClipboardHelper<TCol, TRow>.GetType("DevExpress.XtraExport.Helpers.ClipboardExportManager`2", false);
            if (type == null)
            {
                return null;
            }
            System.Type[] genericArguments = gridView.GetType().GetGenericArguments();
            if (genericArguments.Length != 2)
            {
                foreach (System.Type type3 in gridView.GetType().GetInterfaces())
                {
                    if ((type3.Namespace == "DevExpress.XtraExport.Helpers") && (type3.Name == "IClipboardGridView`2"))
                    {
                        genericArguments = type3.GetGenericArguments();
                        break;
                    }
                }
            }
            object[] args = new object[] { gridView, exportOptions };
            return (Activator.CreateInstance(type.MakeGenericType(genericArguments), args) as IClipboardManager<TCol, TRow>);
        }

        private static Assembly GetPrintingAssembly(bool throwException)
        {
            if (throwException && (ClipboardHelper<TCol, TRow>.printingAssembly == null))
            {
                throw new Exception("DevExpress.XtraPrinting.v19.2 isn't found.");
            }
            return ClipboardHelper<TCol, TRow>.printingAssembly;
        }

        private static System.Type GetType(string typeName, bool throwException)
        {
            Assembly printingAssembly = ClipboardHelper<TCol, TRow>.GetPrintingAssembly(false);
            return ((printingAssembly == null) ? ClipboardHelper<TCol, TRow>.GetTypeOfficially(typeName, throwException) : printingAssembly.GetType(typeName));
        }

        private static System.Type GetTypeOfficially(string typeName, bool throwException) => 
            System.Type.GetType($"{typeName}, {"DevExpress.XtraPrinting.v19.2"}", throwException);

        public static void SetClipboardData(IClipboardManager<TCol, TRow> manager, DataObject dataObject)
        {
            if (manager != null)
            {
                manager.SetClipboardData(dataObject);
            }
        }
    }
}

