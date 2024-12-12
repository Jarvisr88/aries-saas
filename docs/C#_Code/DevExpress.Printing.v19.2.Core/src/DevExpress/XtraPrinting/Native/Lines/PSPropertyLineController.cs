namespace DevExpress.XtraPrinting.Native.Lines
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    public abstract class PSPropertyLineController : BaseLineController
    {
        private string headerText;
        protected PropertyDescriptor property;
        protected object obj;

        protected PSPropertyLineController(PropertyDescriptor property, object obj, string headerText);
        protected RuntimeTypeDescriptorContext CreateTypeDescriptorContext();
        protected virtual TypeStringConverter CreateTypeStringConverter();
        protected override void InitLine();
    }
}

