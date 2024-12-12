namespace DevExpress.SpreadsheetSource.Xls
{
    using System;

    public class XlsSourceCommandFactory5 : XlsSourceCommandFactoryBase
    {
        protected override void InitializeFactory()
        {
            base.InitializeFactory();
            base.Add(0x2f, new XlsSourceCommandFilePassword5());
            base.Add(0x42, new XlsSourceCommandEncoding());
            base.Add(0x85, new XlsSourceCommandBoundSheet5());
            base.Add(0x41e, new XlsSourceCommandFormat5());
            base.Add(0xe0, new XlsSourceCommandXF5());
            base.Add(0x18, new XlsSourceCommandDefinedName5());
            base.Add(0x20b, new XlsSourceCommandIndex5());
            base.Add(0x204, new XlsSourceCommandLabel5());
            base.Add(0xd6, new XlsSourceCommandRichString5());
            base.Add(0x207, new XlsSourceCommandString5());
        }
    }
}

