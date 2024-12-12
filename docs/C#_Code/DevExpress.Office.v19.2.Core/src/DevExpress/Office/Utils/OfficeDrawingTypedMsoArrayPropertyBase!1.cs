namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;

    public abstract class OfficeDrawingTypedMsoArrayPropertyBase<T> : OfficeDrawingMsoArrayPropertyBase, IOfficeDrawingTypedMsoArrayPropertyBase<T>
    {
        private const int MsoArrayHeaderSize = 6;

        protected OfficeDrawingTypedMsoArrayPropertyBase()
        {
        }

        public T[] GetElements()
        {
            if (base.ComplexData.Length < 6)
            {
                return new T[0];
            }
            int num = BitConverter.ToUInt16(base.ComplexData, 0);
            int size = BitConverter.ToUInt16(base.ComplexData, 4);
            if (size == 0xfff0)
            {
                size = 4;
            }
            if (base.ComplexData.Length != ((size * num) + 6))
            {
                return new T[0];
            }
            T[] localArray = new T[num];
            int offset = 6;
            for (int i = 0; i < num; i++)
            {
                localArray[i] = this.ReadElement(offset, size);
                offset += size;
            }
            return localArray;
        }

        public override byte[] GetElementsData()
        {
            if (base.ComplexData.Length < 6)
            {
                return new byte[0];
            }
            byte[] destinationArray = new byte[base.ComplexData.Length - 6];
            Array.Copy(base.ComplexData, 6, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        public abstract T ReadElement(int offset, int size);
        public void SetElements(T[] elements)
        {
            if (elements.Length != 0)
            {
                ushort elementSize = (ushort) this.ElementSize;
                byte[] bytes = BitConverter.GetBytes((ushort) elements.Length);
                List<byte> data = new List<byte>((elements.Length * elementSize) + 6);
                data.AddRange(bytes);
                data.AddRange(bytes);
                data.AddRange(BitConverter.GetBytes(elementSize));
                foreach (T local in elements)
                {
                    this.WriteElement(local, data);
                }
                base.SetComplexData(data.ToArray());
                base.Value = base.ComplexData.Length;
            }
        }

        public abstract void WriteElement(T element, List<byte> data);

        public abstract int ElementSize { get; }
    }
}

