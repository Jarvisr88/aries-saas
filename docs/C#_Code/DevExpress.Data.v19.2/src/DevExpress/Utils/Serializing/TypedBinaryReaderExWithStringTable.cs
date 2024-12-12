namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections.Specialized;
    using System.IO;

    public class TypedBinaryReaderExWithStringTable : TypedBinaryReaderEx
    {
        private int startPosition;
        private int endPosition;
        private int stringTablePosition;
        private StringCollection stringTable;

        public TypedBinaryReaderExWithStringTable(BinaryReader input) : base(input)
        {
            this.startPosition = -1;
            this.endPosition = -1;
            this.stringTablePosition = -1;
            this.stringTable = new StringCollection();
        }

        public override object ReadObject()
        {
            if (this.startPosition == -1)
            {
                int num = base.Input.ReadInt32();
                this.startPosition = (int) base.Input.BaseStream.Position;
                base.Input.BaseStream.Seek((long) num, SeekOrigin.Current);
                this.stringTablePosition = (int) base.Input.BaseStream.Position;
                this.ReadStringTable();
                this.endPosition = (int) base.Input.BaseStream.Position;
                base.Input.BaseStream.Seek((long) this.startPosition, SeekOrigin.Begin);
            }
            object obj2 = base.ReadObject();
            if (base.Input.BaseStream.Position == this.stringTablePosition)
            {
                base.Input.BaseStream.Seek((long) this.endPosition, SeekOrigin.Begin);
            }
            return obj2;
        }

        protected internal override string ReadString()
        {
            int num = (int) this.ReadObject();
            return this.stringTable[num];
        }

        protected internal virtual void ReadStringTable()
        {
            int num = (int) this.ReadObject();
            for (int i = 0; i < num; i++)
            {
                this.stringTable.Add(base.Input.ReadString());
            }
        }
    }
}

