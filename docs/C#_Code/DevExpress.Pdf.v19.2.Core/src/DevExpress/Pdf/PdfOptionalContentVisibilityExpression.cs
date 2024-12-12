namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionalContentVisibilityExpression : PdfOptionalContent
    {
        private readonly PdfOptionalContentVisibilityExpressionOperator operation;
        private readonly List<PdfOptionalContent> operands;

        internal PdfOptionalContentVisibilityExpression(PdfObjectCollection objects, IList<object> array) : base(-1)
        {
            int count = array.Count;
            if (count < 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfName name = array[0] as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.operation = PdfEnumToStringConverter.Parse<PdfOptionalContentVisibilityExpressionOperator>(name.Name, true);
            if ((this.operation == PdfOptionalContentVisibilityExpressionOperator.Not) && (count != 2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.operands = new List<PdfOptionalContent>(count - 1);
            for (int i = 1; i < count; i++)
            {
                object obj2 = objects.TryResolve(array[i], null);
                PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
                if (dictionary == null)
                {
                    IList<object> list = obj2 as IList<object>;
                    if (list == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.operands.Add(new PdfOptionalContentVisibilityExpression(objects, list));
                }
                else
                {
                    PdfOptionalContentGroup item = ParseOptionalContent(dictionary) as PdfOptionalContentGroup;
                    if (item == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.operands.Add(item);
                }
            }
        }

        protected internal override object Write(PdfObjectCollection objects)
        {
            int count = this.operands.Count;
            object[] objArray = new object[] { PdfEnumToStringConverter.Convert<PdfOptionalContentVisibilityExpressionOperator>(this.operation, false) };
            int index = 1;
            int num3 = 0;
            while (index <= count)
            {
                objArray[index] = this.operands[num3++];
                index++;
            }
            return objArray;
        }

        public PdfOptionalContentVisibilityExpressionOperator Operation =>
            this.operation;

        public IList<PdfOptionalContent> Operands =>
            this.operands;
    }
}

