namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class XlsSourceCommandFactoryBase : IXlsSourceCommandFactory
    {
        private const int minRecordSize = 4;
        private readonly Dictionary<int, IXlsSourceCommand> commands = new Dictionary<int, IXlsSourceCommand>();

        protected XlsSourceCommandFactoryBase()
        {
            this.InitializeFactory();
        }

        protected void Add(int typeCode, IXlsSourceCommand command)
        {
            this.commands.Add(typeCode, command);
        }

        public IXlsSourceCommand CreateCommand(XlReader reader)
        {
            long offset = reader.StreamLength - reader.Position;
            if (offset < 4L)
            {
                reader.Seek(offset, SeekOrigin.Current);
                return null;
            }
            int typeCode = reader.ReadNotCryptedInt16();
            return this.CreateCommand(typeCode);
        }

        public IXlsSourceCommand CreateCommand(int typeCode)
        {
            if (!this.commands.ContainsKey(typeCode))
            {
                typeCode = 0;
            }
            return this.commands[typeCode];
        }

        protected virtual void InitializeFactory()
        {
            this.Add(0, new XlsSourceCommandEmpty());
            this.Add(0x809, new XlsSourceCommandBOF());
            this.Add(10, new XlsSourceCommandEOF());
            this.Add(0x22, new XlsSourceCommandDate1904());
            this.Add(60, new XlsSourceCommandContinue());
            this.Add(0x55, new XlsSourceCommandDefaultColumnWidth());
            this.Add(0x7d, new XlsSourceCommandColumnInfo());
            this.Add(0xd7, new XlsSourceCommandDbCell());
            this.Add(520, new XlsSourceCommandRow());
            this.Add(0x201, new XlsSourceCommandBlank());
            this.Add(190, new XlsSourceCommandMulBlank());
            this.Add(0x205, new XlsSourceCommandBoolErr());
            this.Add(0x203, new XlsSourceCommandNumber());
            this.Add(0xbd, new XlsSourceCommandMulRk());
            this.Add(0x27e, new XlsSourceCommandRk());
            this.Add(6, new XlsSourceCommandFormula());
        }
    }
}

