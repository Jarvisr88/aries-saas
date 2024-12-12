namespace DevExpress.Utils.Editors
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms.Design;

    public class UIObjectEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object objValue);
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context);
    }
}

