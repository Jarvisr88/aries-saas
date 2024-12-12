namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class PSTextLineController : PSPropertyLineController
    {
        public PSTextLineController(PropertyDescriptor property, object obj, string headerText);
        protected override ILine CreateLine(LineFactoryBase lineFactory);
    }
}

