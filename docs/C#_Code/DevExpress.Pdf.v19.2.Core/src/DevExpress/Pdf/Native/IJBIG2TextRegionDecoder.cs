namespace DevExpress.Pdf.Native
{
    using System;

    public interface IJBIG2TextRegionDecoder : IJBIG2Decoder
    {
        int DecodeDS();
        int DecodeDT();
        int DecodeFS();
        int DecodeID();
        int DecodeIT();
        int DecodeRDH();
        int DecodeRDW();
        int DecodeRDX();
        int DecodeRDY();
        int DecodeRI();
    }
}

