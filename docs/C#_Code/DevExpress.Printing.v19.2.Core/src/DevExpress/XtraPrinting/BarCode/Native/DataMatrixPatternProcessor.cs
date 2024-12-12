namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public abstract class DataMatrixPatternProcessor : IPatternProcessor
    {
        private DataMatrixSize matrixSize;
        private DataMatrixSize realMatrixSize;
        private List<List<bool>> pattern;
        private byte[] encodeBuf;
        private int encodeDataSize;
        private int protectedDataSize;

        protected DataMatrixPatternProcessor();
        private void CalculatePattern();
        public static DataMatrixPatternProcessor CreateInstance(DataMatrixCompactionMode mode);
        void IPatternProcessor.Assign(IPatternProcessor source);
        void IPatternProcessor.RefreshPattern(object data);
        private void ECC200Placement(int[] places, int dataRows, int dataColumns);
        private void ECC200PlacementBit(int[] places, int rowsTotal, int columsTotal, int row, int column, int point, byte bit);
        private void ECC200PlacementBlock(int[] places, int dataRows, int dataColumns, int row, int column, int point);
        private void ECC200PlacementCornerA(int[] places, int dataRows, int dataColumns, int point);
        private void ECC200PlacementCornerB(int[] places, int dataRows, int dataColumns, int point);
        private void ECC200PlacementCornerC(int[] places, int dataRows, int dataColumns, int point);
        private void ECC200PlacementCornerD(int[] places, int dataRows, int dataColumns, int point);
        protected abstract bool EncodeData(object data, byte[] encodeBuf, out int encodeBufSize);
        private void FillPattern();
        public abstract string GetValidCharSet();
        protected virtual void PadData(byte[] encodeBuf, ref int protectedDataSize);
        private void ProtectData();
        protected byte Randomize253State(byte codewordValue, int codewordPosition);
        protected byte Randomize255State(byte codewordValue, int codewordPosition);
        private void SelectMatrix();

        [DefaultValue(0)]
        public DataMatrixSize MatrixSize { get; set; }

        public DataMatrixSize RealMatrixSize { get; }

        ArrayList IPatternProcessor.Pattern { get; }
    }
}

