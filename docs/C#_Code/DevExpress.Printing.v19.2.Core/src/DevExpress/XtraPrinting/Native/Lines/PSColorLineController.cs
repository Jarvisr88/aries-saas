namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class PSColorLineController : PSPropertyLineController
    {
        public PSColorLineController(PropertyDescriptor property, object obj, string headerText);
        protected override ILine CreateLine(LineFactoryBase lineFactory);
    }
}

