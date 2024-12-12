namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections;

    public class DataMatrixMatrixProperties
    {
        private static Hashtable properties;
        private int symbolHeight;
        private int symbolWidth;
        private int regionHeight;
        private int regionWidth;
        private int codewordsTotal;
        private int blocksTotal;
        private int dataBlock;
        private int rsBlock;

        static DataMatrixMatrixProperties();
        private DataMatrixMatrixProperties(int symbolHeight, int symbolWidth, int regionHeight, int regionWidth, int codewordsTotal, int blocksTotal, int dataBlock, int rsBlock);
        public static DataMatrixSize FindOptimalMatrixSize(int codewordsTotal);
        public int GetDataBlockSize(int block);
        public static DataMatrixMatrixProperties GetProperties(DataMatrixSize matrixSize);

        public int SymbolHeight { get; }

        public int SymbolWidth { get; }

        public int RegionHeight { get; }

        public int RegionWidth { get; }

        public int CodewordsTotal { get; }

        public int BlocksTotal { get; }

        public int DataBlock { get; }

        public int RsBlock { get; }
    }
}

