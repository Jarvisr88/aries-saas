namespace DevExpress.Utils.Editors
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class SimpleTypeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object obj);
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context);
        private static Type GetTypeFromTypeCode(TypeCode value);

        public class SimpleTypeEditorListBox : ListBox
        {
            private IWindowsFormsEditorService edSvc;

            public SimpleTypeEditorListBox(IWindowsFormsEditorService edSvc);
            protected override void OnDoubleClick(EventArgs e);
        }
    }
}

