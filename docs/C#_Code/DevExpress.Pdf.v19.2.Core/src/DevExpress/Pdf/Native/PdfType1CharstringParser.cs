namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfType1CharstringParser : IDisposable
    {
        private readonly PdfBinaryStream stream;

        private PdfType1CharstringParser(byte[] charstring)
        {
            this.stream = new PdfBinaryStream(charstring);
        }

        public void Dispose()
        {
            this.stream.Dispose();
        }

        private IPdfType1CharstringToken GetNextToken()
        {
            byte operationCode = this.stream.ReadByte();
            if ((operationCode >= 0x20) && (operationCode <= 0xf6))
            {
                return new PdfType1CharstringOperand(operationCode - 0x8b);
            }
            if ((operationCode >= 0xf7) && (operationCode <= 250))
            {
                byte num2 = this.stream.ReadByte();
                return new PdfType1CharstringOperand((((operationCode - 0xf7) << 8) + num2) + 0x6c);
            }
            if ((operationCode < 0xfb) || (operationCode > 0xfe))
            {
                return ((operationCode != 0xff) ? ((operationCode != 12) ? ((IPdfType1CharstringToken) PdfType1CharstringSingleByteOperator.CreateSingleByteOperator(operationCode)) : ((IPdfType1CharstringToken) PdfType1CharstringTwoByteOperator.CreateTwoByteOperator(this.stream.ReadByte()))) : ((IPdfType1CharstringToken) new PdfType1CharstringOperand(this.stream.ReadInt())));
            }
            byte num3 = this.stream.ReadByte();
            return new PdfType1CharstringOperand((-((operationCode - 0xfb) << 8) - num3) - 0x6c);
        }

        public static IList<IPdfType1CharstringToken> Parse(byte[] charstring)
        {
            List<IPdfType1CharstringToken> list = new List<IPdfType1CharstringToken>();
            if (charstring != null)
            {
                using (PdfType1CharstringParser parser = new PdfType1CharstringParser(charstring))
                {
                    while (!parser.Ended)
                    {
                        IPdfType1CharstringToken nextToken = parser.GetNextToken();
                        if (nextToken != null)
                        {
                            list.Add(nextToken);
                        }
                    }
                }
            }
            return list;
        }

        public bool Ended =>
            this.stream.Position >= this.stream.Length;
    }
}

