namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfVisitorBasedFactory<TInput, TOutput> where TInput: class where TOutput: class
    {
        private TOutput result;

        protected PdfVisitorBasedFactory()
        {
        }

        public TOutput Create(TInput input)
        {
            this.result = default(TOutput);
            this.Visit(input);
            return this.result;
        }

        protected void SetResult(TOutput result)
        {
            this.result = result;
        }

        protected abstract void Visit(TInput input);
    }
}

