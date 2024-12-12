namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections.Specialized;
    using System.IO;

    public class TypedBinaryWriterExWithStringTable : TypedBinaryWriterEx
    {
        private bool closed;
        private int startPosition;
        private StringCollection stringTable;

        public TypedBinaryWriterExWithStringTable(BinaryWriter output) : base(output)
        {
            this.startPosition = -1;
            this.stringTable = new StringCollection();
        }

        public override void Close()
        {
            if (!this.closed)
            {
                this.Flush();
                int position = (int) base.Output.BaseStream.Position;
                base.Output.Seek(this.startPosition - 4, SeekOrigin.Begin);
                base.Output.Write((int) (position - this.startPosition));
                this.Flush();
                base.Output.Seek(position, SeekOrigin.Begin);
                this.WriteStringTable();
                this.closed = true;
            }
            base.Close();
        }

        private void CorrectStartPosition()
        {
            if (this.startPosition == -1)
            {
                base.Output.Write(0);
                this.startPosition = (int) base.Output.BaseStream.Position;
            }
        }

        public void WriteCustomObject(Type type, string serializedObject)
        {
            this.CorrectStartPosition();
            base.WriteObjectCore(type, serializedObject, true);
        }

        public override void WriteObject(object obj)
        {
            this.CorrectStartPosition();
            base.WriteObject(obj);
        }

        protected internal override void WriteString(string val)
        {
            base.Output.Write((byte) 0x77);
            int index = this.stringTable.IndexOf(val);
            if (index < 0)
            {
                index = this.stringTable.Count;
                this.stringTable.Add(val);
            }
            this.WriteInt32(index);
        }

        protected internal virtual void WriteStringTable()
        {
            int count = this.stringTable.Count;
            this.WriteInt32(count);
            for (int i = 0; i < count; i++)
            {
                base.Output.Write(this.stringTable[i]);
            }
        }
    }
}

