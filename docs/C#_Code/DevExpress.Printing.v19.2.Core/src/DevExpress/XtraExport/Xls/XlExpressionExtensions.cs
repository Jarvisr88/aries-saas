namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal static class XlExpressionExtensions
    {
        public static byte[] GetBytes(this XlExpression expression, XlsDataAwareExporter exporter)
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    expression.Write(writer, exporter);
                    buffer = stream.ToArray();
                }
            }
            return buffer;
        }

        public static XlExpression ToXlsExpression(this XlExpression expression, XlsDataAwareExporter exporter) => 
            new XlsPtgConverter(exporter).Convert(expression);

        public static void Write(this XlExpression expression, BinaryWriter writer, XlsDataAwareExporter exporter)
        {
            new XlsPtgBinaryWriter(writer, exporter).Write(expression);
        }
    }
}

