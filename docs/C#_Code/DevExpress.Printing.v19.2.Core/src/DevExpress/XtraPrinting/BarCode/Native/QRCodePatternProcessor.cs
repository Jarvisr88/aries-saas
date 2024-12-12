namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [CLSCompliant(false)]
    public abstract class QRCodePatternProcessor : IPatternProcessor
    {
        private List<List<bool>> pattern;
        private QRCodeErrorCorrectionLevel errorCorrectionLevel;
        private QRCodeVersion version;
        private byte[] data;
        private int[] codeWordNumPlus;

        protected QRCodePatternProcessor();
        private void CalculatePattern();
        private void CreateEmptyPattern();
        public static QRCodePatternProcessor CreateInstance(QRCodeCompactionMode mode);
        void IPatternProcessor.Assign(IPatternProcessor source);
        void IPatternProcessor.RefreshPattern(object data);
        private void FillPattern(int sideModules, sbyte[] frameData, sbyte[][] matrixContent, sbyte maskContent);
        private int GetAutoSizeVersion(int totalDataBits, int ec, ref int maxDataBits);
        protected abstract int GetCodeWordCount(int[] dataValue, ref int dataCounter, int dataLength, sbyte[] dataBits);
        protected abstract int[] GetCodeWordNumPlus();
        internal virtual int GetMaxDataLength();
        private int GetMaxLogoInvalidModuleCount(int ec, int sizeVersion);
        public abstract string GetValidCharset();
        private int GetValidSizeVersion(int totalDataBits, int errorCorrectionLevel, ref int maxDataBits);
        public abstract bool IsValidData(object data);
        internal bool IsValidLogoSettings(float logoModuleCount);
        protected abstract void SetData(object data);

        protected byte[] Data { get; set; }

        [DefaultValue(1)]
        public QRCodeErrorCorrectionLevel ErrorCorrectionLevel { get; set; }

        [DefaultValue(0)]
        public QRCodeVersion Version { get; set; }

        public int LogoModuleCount { get; set; }

        ArrayList IPatternProcessor.Pattern { get; }
    }
}

