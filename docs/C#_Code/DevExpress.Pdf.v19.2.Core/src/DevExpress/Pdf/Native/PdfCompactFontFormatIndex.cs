namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfCompactFontFormatIndex
    {
        protected PdfCompactFontFormatIndex()
        {
        }

        protected PdfCompactFontFormatIndex(PdfBinaryStream stream)
        {
            int objectsCount = (ushort) stream.ReadShort();
            this.ProcessObjectsCount(objectsCount);
            if (objectsCount > 0)
            {
                int length = stream.ReadByte();
                int[] numArray = new int[objectsCount + 1];
                int index = 0;
                while (true)
                {
                    if (index > objectsCount)
                    {
                        int num3 = numArray[0];
                        int num5 = 1;
                        for (int i = 0; num5 <= objectsCount; i++)
                        {
                            int num7 = numArray[num5];
                            int num8 = num7 - num3;
                            if (num8 < 0)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.ProcessObject(i, (num8 > 0) ? stream.ReadArray(num8) : new byte[0]);
                            num3 = num7;
                            num5++;
                        }
                        break;
                    }
                    numArray[index] = stream.ReadOffSet(length);
                    index++;
                }
            }
        }

        protected abstract int GetObjectLength(int index);
        protected abstract void ProcessObject(int index, byte[] objectData);
        protected abstract void ProcessObjectsCount(int objectsCount);
        public void Write(PdfBinaryStream stream)
        {
            int objectsCount = this.ObjectsCount;
            stream.WriteUShort(objectsCount);
            if (objectsCount > 0)
            {
                stream.WriteByte(4);
                int num2 = 1;
                stream.WriteInt(num2);
                int index = 0;
                while (true)
                {
                    if (index >= objectsCount)
                    {
                        for (int i = 0; i < objectsCount; i++)
                        {
                            this.WriteObject(stream, i);
                        }
                        break;
                    }
                    num2 += this.GetObjectLength(index);
                    stream.WriteInt(num2);
                    index++;
                }
            }
        }

        protected abstract void WriteObject(PdfBinaryStream stream, int index);

        public int DataLength
        {
            get
            {
                int objectsCount = this.ObjectsCount;
                int num2 = (objectsCount > 0) ? (((objectsCount + 1) * 4) + 3) : 2;
                for (int i = 0; i < objectsCount; i++)
                {
                    num2 += this.GetObjectLength(i);
                }
                return num2;
            }
        }

        protected abstract int ObjectsCount { get; }
    }
}

