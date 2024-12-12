namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class PdfType1CharstringSingleByteOperator : IPdfType1CharstringToken
    {
        private readonly byte operationCode;

        protected PdfType1CharstringSingleByteOperator(byte operationCode)
        {
            this.operationCode = operationCode;
        }

        public static PdfType1CharstringSingleByteOperator CreateSingleByteOperator(byte operationCode)
        {
            switch (operationCode)
            {
                case 1:
                    return new PdfType1CharstringHstemOperator();

                case 3:
                    return new PdfType1CharstringVstemOperator();

                case 4:
                    return new PdfType1CharstringVMoveToOperator();

                case 5:
                    return new PdfType1CharstringRLineToOperator();

                case 6:
                    return new PdfType1CharstringHLineToOperator();

                case 7:
                    return new PdfType1CharstringVLineToOperator();

                case 8:
                    return new PdfType1CharstringRRCurveToOperator();

                case 9:
                    return new PdfType1CharstringClosepathOperator();

                case 10:
                    return new PdfType1CharstringCallsubrOperator();

                case 11:
                    return new PdfType1CharstringReturnOperator();

                case 13:
                    return new PdfType1CharstringHsbwOperator();

                case 14:
                    return new PdfType1CharstringEndcharOperator();

                case 0x15:
                    return new PdfType1CharstringRMoveToOperator();

                case 0x16:
                    return new PdfType1CharstringHMoveToOperator();

                case 30:
                    return new PdfType1CharstringVHCurveToOperator();

                case 0x1f:
                    return new PdfType1CharstringHVCurveToOperator();
            }
            return null;
        }

        public abstract void Execute(PdfType1CharstringInterpreter interpreter);

        public byte OperationCode =>
            this.operationCode;
    }
}

