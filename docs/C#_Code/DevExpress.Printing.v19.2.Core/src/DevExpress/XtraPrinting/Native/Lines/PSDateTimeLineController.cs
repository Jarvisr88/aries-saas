namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class PSDateTimeLineController : PSEditorLineController
    {
        public PSDateTimeLineController(PropertyDescriptor property, object obj, string headerText);
        protected override ILine CreateLine(LineFactoryBase lineFactory);
    }
}

