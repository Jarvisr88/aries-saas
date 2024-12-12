namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1CharstringTwoByteOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte TwoByteOperatorPrefix = 12;

        protected PdfType1CharstringTwoByteOperator(byte operationCode) : base(operationCode)
        {
        }

        public static PdfType1CharstringTwoByteOperator CreateTwoByteOperator(byte operationCode)
        {
            if (operationCode > 12)
            {
                if (operationCode == 0x10)
                {
                    return new PdfType1CharstringCallOthersubrOperator();
                }
                if (operationCode == 0x11)
                {
                    return new PdfType1CharstringPopOperator();
                }
                if (operationCode == 0x21)
                {
                    return new PdfType1CharstringSetCurrentPointOperator();
                }
            }
            else
            {
                switch (operationCode)
                {
                    case 0:
                        return new PdfType1CharstringDotsectionOperator();

                    case 1:
                        return new PdfType1CharstringVstem3Operator();

                    case 2:
                        return new PdfType1CharstringHstem3Operator();

                    case 3:
                    case 4:
                    case 5:
                        break;

                    case 6:
                        return new PdfType1CharstringSeacOperator();

                    case 7:
                        return new PdfType1CharstringSbwOperator();

                    default:
                        if (operationCode != 12)
                        {
                            break;
                        }
                        return new PdfType1CharstringDivOperator();
                }
            }
            return null;
        }
    }
}

