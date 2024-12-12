namespace DevExpress.SpreadsheetSource.Xls
{
    using System;

    public class XlsSourceCommandFactory : XlsSourceCommandFactoryBase
    {
        protected override void InitializeFactory()
        {
            base.InitializeFactory();
            base.Add(0x2f, new XlsSourceCommandFilePassword());
            base.Add(0x85, new XlsSourceCommandBoundSheet8());
            base.Add(0x41e, new XlsSourceCommandFormat());
            base.Add(0xe0, new XlsSourceCommandXF());
            base.Add(0xfc, new XlsSourceCommandSharedStrings());
            base.Add(430, new XlsSourceCommandSupBook());
            base.Add(0x17, new XlsSourceCommandExternSheet());
            base.Add(0x18, new XlsSourceCommandDefinedName());
            base.Add(0x894, new XlsSourceCommandNameComment());
            base.Add(0x20b, new XlsSourceCommandIndex());
            base.Add(0x204, new XlsSourceCommandLabel());
            base.Add(0xfd, new XlsSourceCommandLabelSst());
            base.Add(0x207, new XlsSourceCommandString());
            base.Add(0x872, new XlsSourceCommandFeature11());
            base.Add(0x878, new XlsSourceCommandFeature11());
        }
    }
}

