namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class PSBooleanLineController : PSPropertyLineController
    {
        public PSBooleanLineController(PropertyDescriptor property, object obj, string headerText);
        protected override ILine CreateLine(LineFactoryBase lineFactory);
    }
}

