namespace DevExpress.Utils.Internal
{
    using System;

    internal class CharMapper4Segment
    {
        public ushort StartCode;
        public ushort EndCode;
        public short IdDelta;
        public ushort[] IndexArray;

        public int GetGlyphIndex(char chr)
        {
            int num = chr;
            if (this.IndexArray == null)
            {
                return (ushort) (((ushort) num) + ((ushort) this.IdDelta));
            }
            int num3 = num - this.StartCode;
            return (((num3 < 0) || (num3 >= this.IndexArray.Length)) ? -1 : this.IndexArray[num - this.StartCode]);
        }

        public bool IsCharFallInRange(ushort chrIndex) => 
            (chrIndex >= this.StartCode) && (chrIndex <= this.EndCode);

        public void ReadInternal(BigEndianStreamReader reader)
        {
            this.StartCode = reader.ReadUShort();
            this.EndCode = reader.ReadUShort();
            this.IdDelta = reader.ReadShort();
            if (this.IdDelta != 0)
            {
                this.IndexArray = null;
            }
            else
            {
                int num = reader.ReadUShort();
                this.IndexArray = new ushort[num];
                for (int i = 0; i < num; i++)
                {
                    this.IndexArray[i] = reader.ReadUShort();
                }
            }
        }

        public override string ToString()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "[";
            textArray1[1] = this.StartCode.ToString();
            textArray1[2] = ", ";
            textArray1[3] = this.EndCode.ToString();
            textArray1[4] = "] -> [";
            textArray1[5] = (this.StartCode + this.IdDelta).ToString();
            textArray1[6] = ", ";
            textArray1[7] = (this.EndCode + this.IdDelta).ToString();
            textArray1[8] = "]";
            return string.Concat(textArray1);
        }

        public void WriteInternal(BigEndianStreamWriter writer)
        {
            writer.WriteUShort(this.StartCode);
            writer.WriteUShort(this.EndCode);
            writer.WriteShort(this.IdDelta);
            if (this.IdDelta == 0)
            {
                writer.WriteUShort((ushort) this.IndexArray.Length);
                for (int i = 0; i < this.IndexArray.Length; i++)
                {
                    writer.WriteUShort(this.IndexArray[i]);
                }
            }
        }
    }
}

