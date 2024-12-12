namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class PdfWritableArray<T> : IPdfWritableObject, IEnumerable
    {
        private readonly IEnumerable<T> value;

        protected PdfWritableArray(IEnumerable<T> value)
        {
            this.value = value;
        }

        public IEnumerator GetEnumerator() => 
            this.value.GetEnumerator();

        public void Write(PdfDocumentStream stream, int number)
        {
            stream.WriteOpenBracket();
            using (IEnumerator<T> enumerator = this.value.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    this.WriteItem(stream, enumerator.Current, number);
                }
                while (enumerator.MoveNext())
                {
                    stream.WriteSpace();
                    this.WriteItem(stream, enumerator.Current, number);
                }
            }
            stream.WriteCloseBracket();
        }

        protected abstract void WriteItem(PdfDocumentStream documentStream, T value, int number);
    }
}

