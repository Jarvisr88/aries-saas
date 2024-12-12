namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfStack
    {
        private readonly List<object> list = new List<object>();
        private int count;

        public void Clear()
        {
            this.count = 0;
        }

        public void Exchange()
        {
            if (this.count < 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = this.count - 1;
            int num2 = num - 1;
            this.list[num] = this.list[num2];
            this.list[num2] = this.list[num];
        }

        public object Peek() => 
            this.PeekAtIndex(0);

        public object PeekAtIndex(int index)
        {
            if ((index < 0) || (index >= this.count))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return this.list[(this.count - index) - 1];
        }

        public object Pop(bool throwException = true)
        {
            if (this.count > 0)
            {
                int num = this.count - 1;
                this.count = num;
                return this.list[num];
            }
            if (throwException)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return null;
        }

        public double PopDouble() => 
            PdfDocumentReader.ConvertToDouble(this.Pop(true));

        public int PopInt()
        {
            object obj2 = this.Pop(true);
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (int) obj2;
        }

        public string PopName(bool throwException = true)
        {
            PdfName name = this.Pop(throwException) as PdfName;
            if (throwException && (name == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return name?.Name;
        }

        public void Push(object obj)
        {
            if (this.count >= this.list.Count)
            {
                this.list.Add(obj);
                this.count++;
            }
            else
            {
                int count = this.count;
                this.count = count + 1;
                this.list[count] = obj;
            }
        }

        public int Count =>
            this.count;
    }
}

