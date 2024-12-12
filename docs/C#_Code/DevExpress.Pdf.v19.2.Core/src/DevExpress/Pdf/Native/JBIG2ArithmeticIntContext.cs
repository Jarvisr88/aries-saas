namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2ArithmeticIntContext
    {
        private byte[] iax = new byte[0x200];

        public PdfArithmeticDecoderResult Decode(PdfArithmeticState state)
        {
            int num5;
            int num6;
            int index = 1;
            int num2 = state.Decode(this.iax, index);
            index = (index << 1) | num2;
            int num4 = state.Decode(this.iax, index);
            index = (index << 1) | num4;
            if (num4 == 0)
            {
                num5 = 2;
                num6 = 0;
            }
            else
            {
                num4 = state.Decode(this.iax, index);
                index = (index << 1) | num4;
                if (num4 == 0)
                {
                    num5 = 4;
                    num6 = 4;
                }
                else
                {
                    num4 = state.Decode(this.iax, index);
                    index = (index << 1) | num4;
                    if (num4 == 0)
                    {
                        num5 = 6;
                        num6 = 20;
                    }
                    else
                    {
                        num4 = state.Decode(this.iax, index);
                        index = (index << 1) | num4;
                        if (num4 == 0)
                        {
                            num5 = 8;
                            num6 = 0x54;
                        }
                        else
                        {
                            num4 = state.Decode(this.iax, index);
                            index = (index << 1) | num4;
                            if (num4 != 0)
                            {
                                num5 = 0x20;
                                num6 = 0x1154;
                            }
                            else
                            {
                                num5 = 12;
                                num6 = 340;
                            }
                        }
                    }
                }
            }
            int result = 0;
            for (int i = 0; i < num5; i++)
            {
                num4 = state.Decode(this.iax, index);
                index = (((index << 1) & 0x1ff) | (index & 0x100)) | num4;
                result = (result << 1) | num4;
            }
            result += num6;
            result = (num2 != 0) ? -result : result;
            return new PdfArithmeticDecoderResult(result, (num2 != 0) && (result == 0));
        }
    }
}

