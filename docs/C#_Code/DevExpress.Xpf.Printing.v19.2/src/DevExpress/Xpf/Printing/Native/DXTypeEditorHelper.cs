namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils.Design;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    internal static class DXTypeEditorHelper
    {
        static DXTypeEditorHelper()
        {
            DXAssemblyResolverEx.Init();
        }

        public static IDXTypeEditor GetEditor(object component) => 
            (component != null) ? (!(component is PdfPasswordSecurityOptions) ? (TypeDescriptor.GetEditor(component, typeof(IDXTypeEditor)) as IDXTypeEditor) : new PdfPasswordSecurityOptionsEditor()) : null;
    }
}

