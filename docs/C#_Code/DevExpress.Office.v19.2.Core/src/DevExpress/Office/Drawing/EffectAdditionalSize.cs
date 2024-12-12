namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct EffectAdditionalSize
    {
        private static byte MaskProcessGlowSize;
        private static byte MaskProcessBlurSize;
        private static byte MaskProcessSoftEdgesSize;
        private const byte DefaultValues = 3;
        public static EffectAdditionalSize Empty;
        public static EffectAdditionalSize Default;
        private float glowSize;
        private float blurSize;
        private byte packedValues;
        private EffectAdditionalSize(byte packedValues)
        {
            this = new EffectAdditionalSize();
            this.packedValues = packedValues;
        }

        public EffectAdditionalSize(float glowSize, bool processSoftEdgesSize)
        {
            this = new EffectAdditionalSize();
            this.blurSize = 0f;
            this.glowSize = glowSize;
            this.packedValues = 3;
            this.ProcessSoftEdgesSize = processSoftEdgesSize;
        }

        public float GlowSize =>
            this.glowSize;
        public float BlurSize =>
            this.blurSize;
        public float TotalSize =>
            this.glowSize + this.blurSize;
        public bool ProcessGlowSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, MaskProcessGlowSize);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, MaskProcessGlowSize, value);
        }
        public bool ProcessBlurSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, MaskProcessBlurSize);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, MaskProcessBlurSize, value);
        }
        public bool ProcessSoftEdgesSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, MaskProcessSoftEdgesSize);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, MaskProcessSoftEdgesSize, value);
        }
        public void AddSize(IShapeLayoutInfo shapeLayoutInfo)
        {
            float num = this.ProcessBlurSize ? shapeLayoutInfo.BlurSize : 0f;
            float num2 = this.ProcessGlowSize ? shapeLayoutInfo.GlowSize : 0f;
            if ((this.blurSize > 0f) && (this.glowSize > 0f))
            {
                num2 *= 0.7f;
            }
            this.glowSize += num2;
            this.blurSize += num;
            float softEdgesSize = shapeLayoutInfo.SoftEdgesSize;
            if (this.ProcessSoftEdgesSize && (softEdgesSize > 0f))
            {
                if (this.glowSize > 0f)
                {
                    this.glowSize -= softEdgesSize;
                    if (this.glowSize >= 0f)
                    {
                        softEdgesSize = 0f;
                    }
                    else
                    {
                        softEdgesSize = -this.glowSize;
                        this.glowSize = 0f;
                    }
                }
                if (this.blurSize > 0f)
                {
                    this.blurSize -= softEdgesSize;
                    if (this.blurSize < 0f)
                    {
                        this.blurSize = 0f;
                    }
                }
            }
        }

        static EffectAdditionalSize()
        {
            MaskProcessGlowSize = 1;
            MaskProcessBlurSize = 2;
            MaskProcessSoftEdgesSize = 4;
            Empty = new EffectAdditionalSize();
            Default = new EffectAdditionalSize(3);
        }
    }
}

