namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrintingLinks;
    using System;
    using System.Drawing.Printing;
    using System.Reflection;
    using System.Windows.Forms;

    public class ComponentPrinterDynamic : ComponentPrinterBase
    {
        public ComponentPrinterDynamic(IPrintable component) : base(component)
        {
        }

        public ComponentPrinterDynamic(IPrintable component, PrintingSystemBase printingSystem) : base(component, printingSystem)
        {
        }

        protected override PrintableComponentLinkBase CreateLink()
        {
            System.Type type = GetType("DevExpress.XtraPrinting.PrintableComponentLink", false);
            return ((type != null) ? ((PrintableComponentLinkBase) Activator.CreateInstance(type)) : base.CreateLink());
        }

        private object CreatePrintTool()
        {
            object[] args = new object[] { base.LinkBase };
            return Activator.CreateInstance(GetType("DevExpress.XtraPrinting.Links.LinkPrintTool", true), args);
        }

        private static System.Type GetType(string typeName, bool throwException)
        {
            Assembly printingAssembly = SingletonContainer.GetPrintingAssembly(false);
            return ((printingAssembly == null) ? GetTypeOfficially(typeName, throwException) : printingAssembly.GetType(typeName));
        }

        private static System.Type GetTypeOfficially(string typeName, bool throwException) => 
            System.Type.GetType($"{typeName}, {"DevExpress.XtraPrinting.v19.2"}", throwException);

        private void InvokeLinkMethod(string memberName, object[] args)
        {
            if (IsPrintingAvailable(true))
            {
                base.LinkBase.GetType().InvokeMember(memberName, BindingFlags.InvokeMethod, null, base.LinkBase, args);
            }
        }

        internal static bool IsPrintingAvailable_(bool throwException) => 
            SingletonContainer.GetPrintingAssembly(throwException) != null;

        public override void Print()
        {
            object[] args = new object[] { "" };
            this.InvokeLinkMethod("Print", args);
        }

        public override void PrintDialog()
        {
            this.InvokeLinkMethod("PrintDlg", new object[0]);
        }

        public override Form ShowPreview(object lookAndFeel) => 
            this.ShowPreviewCore("ShowPreview", "PreviewForm", lookAndFeel);

        public override Form ShowPreview(IWin32Window owner, object lookAndFeel) => 
            this.ShowPreviewCore("ShowPreview", "PreviewForm", owner, lookAndFeel);

        private Form ShowPreviewCore(string method, string form, object lookAndFeel)
        {
            object target = this.CreatePrintTool();
            object[] args = new object[] { lookAndFeel };
            target.GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, target, args);
            return (target.GetType().InvokeMember(form, BindingFlags.GetProperty, null, target, null) as Form);
        }

        private Form ShowPreviewCore(string method, string form, IWin32Window owner, object lookAndFeel)
        {
            object target = this.CreatePrintTool();
            object[] args = new object[] { owner, lookAndFeel };
            target.GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, target, args);
            return (target.GetType().InvokeMember(form, BindingFlags.GetProperty, null, target, null) as Form);
        }

        public override Form ShowRibbonPreview(object lookAndFeel) => 
            this.ShowPreviewCore("ShowRibbonPreview", "PreviewRibbonForm", lookAndFeel);

        public override Form ShowRibbonPreview(IWin32Window owner, object lookAndFeel) => 
            this.ShowPreviewCore("ShowRibbonPreview", "PreviewRibbonForm", owner, lookAndFeel);

        public override System.Drawing.Printing.PageSettings PageSettings
        {
            get
            {
                object[] args = new object[] { base.PrintingSystemBase };
                return (SingletonContainer.GetPrintingAssembly(true).GetType("DevExpress.XtraPrinting.PrintingSystemBaseExtensions").InvokeMember("Extend", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, args) as IPrintingSystemExtenderBase).PageSettings;
            }
        }

        private class SingletonContainer
        {
            private static Assembly printingAssembly = AssemblyCache.LoadDXAssembly("DevExpress.XtraPrinting.v19.2");

            public static Assembly GetPrintingAssembly(bool throwException)
            {
                if (throwException && (printingAssembly == null))
                {
                    throw new Exception("DevExpress.XtraPrinting.v19.2 isn't found.");
                }
                return printingAssembly;
            }
        }
    }
}

