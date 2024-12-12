namespace DevExpress.Pdf.Native
{
    using System;

    public class JBIG2Decoder : IJBIG2SymbolDictionaryDecoder, IJBIG2Decoder, IJBIG2TextRegionDecoder
    {
        private readonly PdfArithmeticContext gb;
        private readonly PdfArithmeticContext gr;
        private readonly PdfArithmeticContext iaid;
        private readonly int idLength;
        private readonly JBIG2ArithmeticIntContext iardw = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iardh = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iardx = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iardy = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iadh = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iadw = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iaex = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iaai = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iadt = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iafs = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iads = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iait = new JBIG2ArithmeticIntContext();
        private readonly JBIG2ArithmeticIntContext iari = new JBIG2ArithmeticIntContext();
        private readonly PdfArithmeticState arithmeticState;
        private bool lastCode;

        public JBIG2Decoder(JBIG2StreamHelper sh, int idLength, int gbLength, int grLength)
        {
            this.arithmeticState = new PdfArithmeticState(sh);
            this.iaid = new PdfArithmeticContext(this.arithmeticState, idLength);
            this.idLength = idLength;
            this.gb = new PdfArithmeticContext(this.arithmeticState, gbLength);
            this.gr = new PdfArithmeticContext(this.arithmeticState, grLength);
        }

        public static JBIG2Decoder Create(JBIG2StreamHelper sh, int maxId) => 
            new JBIG2Decoder(sh, FindLength(maxId), 0x10, 0x10);

        private int Decode(JBIG2ArithmeticIntContext ctx)
        {
            PdfArithmeticDecoderResult result = ctx.Decode(this.arithmeticState);
            this.lastCode = result.Code;
            return result.Result;
        }

        public int DecodeAI() => 
            this.Decode(this.iaai);

        public int DecodeDH() => 
            this.Decode(this.iadh);

        public int DecodeDS() => 
            this.Decode(this.iads);

        public int DecodeDT() => 
            this.Decode(this.iadt);

        public int DecodeDW() => 
            this.Decode(this.iadw);

        public int DecodeEX() => 
            this.Decode(this.iaex);

        public int DecodeFS() => 
            this.Decode(this.iafs);

        public int DecodeGB(int context) => 
            this.gb.DecodeBit(context);

        public int DecodeGR(int context) => 
            this.gr.DecodeBit(context);

        public int DecodeID()
        {
            this.lastCode = false;
            int cx = 1;
            for (int i = 0; i < this.idLength; i++)
            {
                int num3 = this.iaid.DecodeBit(cx);
                cx = (cx << 1) | num3;
            }
            return (cx & ((1 << (this.idLength & 0x1f)) - 1));
        }

        public int DecodeIT() => 
            this.Decode(this.iait);

        public int DecodeRDH() => 
            this.Decode(this.iardh);

        public int DecodeRDW() => 
            this.Decode(this.iardw);

        public int DecodeRDX() => 
            this.Decode(this.iardx);

        public int DecodeRDY() => 
            this.Decode(this.iardy);

        public int DecodeRI() => 
            this.Decode(this.iari);

        private static int FindLength(int tmp)
        {
            int num = (tmp == 0) ? 0 : ((int) Math.Ceiling(Math.Log((double) tmp, 2.0)));
            if (num > 30)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return num;
        }

        public bool LastCode =>
            this.lastCode;
    }
}

