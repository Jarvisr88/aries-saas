namespace DevExpress.XtraExport.Xls
{
    using System;

    public static class XlsFileHistory
    {
        public static int Win = 1;
        public static int RISC = 2;
        public static int Beta = 4;
        public static int WinAny = 8;
        public static int MacAny = 0x10;
        public static int BetaAny = 0x20;
        public static int Unused = 0xc0;
        public static int RISCAny = 0x100;
        public static int OutOfMemoryFailure = 0x200;
        public static int OutOfMemoryFailureDuringRendering = 0x400;
        public static int FontLimit = 0x2000;
        public static int Excel2010 = 0x18000;
        public static int Default = (((Win | WinAny) | Unused) | Excel2010);
    }
}

