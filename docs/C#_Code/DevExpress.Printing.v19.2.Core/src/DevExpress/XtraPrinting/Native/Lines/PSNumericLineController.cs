namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class PSNumericLineController : PSPropertyLineController
    {
        public PSNumericLineController(PropertyDescriptor property, object obj, string headerText);
        protected override ILine CreateLine(LineFactoryBase lineFactory);
    }
}

