namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public sealed class PdfPredefinedFunction : PdfFunction
    {
        internal const string DefaultFunctionName = "Default";
        internal const string IdentityFunctionName = "Identity";
        private static readonly PdfPredefinedFunction defaultFunction = new PdfPredefinedFunction();
        private static readonly PdfPredefinedFunction identityFunction = new PdfPredefinedFunction();

        protected internal override bool IsSame(PdfFunction function) => 
            ReferenceEquals(this, function);

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.Write(objects);

        protected internal override double[] Transform(double[] arguments) => 
            arguments;

        protected internal override object Write(PdfObjectCollection objects) => 
            new PdfName(ReferenceEquals(this, defaultFunction) ? "Default" : "Identity");

        public static PdfFunction Default =>
            defaultFunction;

        public static PdfFunction Identity =>
            identityFunction;
    }
}

