namespace DevExpress.Pdf.Native
{
    using System;

    public interface IJBIG2SymbolDictionaryDecoder : IJBIG2Decoder
    {
        int DecodeAI();
        int DecodeDH();
        int DecodeDW();
        int DecodeEX();
    }
}

