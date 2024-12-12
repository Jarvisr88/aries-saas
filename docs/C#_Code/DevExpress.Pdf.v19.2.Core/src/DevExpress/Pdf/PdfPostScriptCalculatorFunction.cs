namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptCalculatorFunction : PdfCustomFunction
    {
        internal const int Number = 4;
        private readonly byte[] code;
        private PdfPostScriptInterpreter interpreter;
        private IEnumerable<object> program;

        internal PdfPostScriptCalculatorFunction(PdfReaderDictionary dictionary, byte[] code) : base(dictionary)
        {
            if (code == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.code = code;
        }

        protected internal override bool IsSame(PdfFunction function) => 
            base.IsSame(function) && (this.code == ((PdfPostScriptCalculatorFunction) function).code);

        protected override double[] PerformTransformation(double[] arguments)
        {
            if (this.program == null)
            {
                try
                {
                    IList<object> list = PdfPostScriptFileParser.Parse(this.code);
                    if ((list != null) && (list.Count != 1))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.program = list[0] as IList<object>;
                    if (this.program == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                catch
                {
                    this.program = new object[0];
                }
                this.interpreter = new PdfPostScriptInterpreter();
            }
            PdfStack stack = this.interpreter.Stack;
            foreach (double num3 in arguments)
            {
                stack.Push(num3);
            }
            this.interpreter.Execute(this.program);
            int rangeSize = this.RangeSize;
            double[] numArray = new double[rangeSize];
            for (int i = rangeSize - 1; i >= 0; i--)
            {
                numArray[i] = PdfDocumentReader.ConvertToDouble(stack.Pop(true));
            }
            stack.Clear();
            return numArray;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(this.FillDictionary(objects), this.code);

        public byte[] Code =>
            this.code;

        protected override int FunctionType =>
            4;
    }
}

