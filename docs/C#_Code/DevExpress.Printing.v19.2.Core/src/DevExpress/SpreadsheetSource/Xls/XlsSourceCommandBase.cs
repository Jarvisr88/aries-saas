namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XlsSourceCommandBase : IXlsSourceCommand
    {
        protected XlsSourceCommandBase()
        {
        }

        protected virtual void CheckPosition(XlReader reader, long initialPosition, long expectedPosition)
        {
            long position = reader.Position;
            if (position < expectedPosition)
            {
                reader.Seek(expectedPosition - position, SeekOrigin.Current);
            }
            else if (position > expectedPosition)
            {
                throw new InvalidFileException(InvalidFileError.CorruptedFile, $"Read failure: initial/expected/actual positions = {initialPosition}/{expectedPosition}/{position}, command={base.GetType().ToString()}");
            }
        }

        public virtual void Execute(XlsSourceDataReader dataReader)
        {
        }

        public virtual void Execute(XlsSpreadsheetSource contentBuilder)
        {
        }

        public void Read(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.Size = reader.ReadNotCryptedUInt16();
            if (this.Size > 0x2020)
            {
                throw new InvalidFileException(InvalidFileError.CorruptedFile, $"Record data size greater than {0x2020}");
            }
            long position = reader.Position;
            reader.SetRecordSize(this.Size);
            this.ReadCore(reader, contentBuilder);
            this.CheckPosition(reader, position, position + this.Size);
        }

        protected abstract void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder);

        protected int Size { get; private set; }

        public virtual bool IsSubstreamBound =>
            false;
    }
}

