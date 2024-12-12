namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXTileComponent : JPXArea
    {
        private readonly JPXCodingStyleComponent codingStyle;
        private readonly int bitsPerComponent;
        private readonly JPXBaseResolutionLevel baseResolutionLevel;
        private readonly JPXCompositeResolutionLevel[] resolutionLevels;
        private JPXQuantizationComponentParameters quantizationParameters;

        public JPXTileComponent(JPXTile tile, JPXComponent component, JPXCodingStyleComponent codingStyle, JPXQuantizationComponentParameters quantizationParameters)
        {
            this.codingStyle = codingStyle;
            this.quantizationParameters = quantizationParameters;
            this.bitsPerComponent = component.BitsPerComponent;
            float horizontalSeparation = component.HorizontalSeparation;
            base.X0 = (int) Math.Ceiling((double) (((float) tile.X0) / horizontalSeparation));
            base.X1 = (int) Math.Ceiling((double) (((float) tile.X1) / horizontalSeparation));
            float verticalSeparation = component.VerticalSeparation;
            base.Y0 = (int) Math.Ceiling((double) (((float) tile.Y0) / verticalSeparation));
            base.Y1 = (int) Math.Ceiling((double) (((float) tile.Y1) / verticalSeparation));
            int decompositionLevelCount = codingStyle.DecompositionLevelCount;
            this.resolutionLevels = new JPXCompositeResolutionLevel[decompositionLevelCount];
            this.baseResolutionLevel = new JPXBaseResolutionLevel(this, 0, codingStyle);
            for (int i = 0; i < decompositionLevelCount; i++)
            {
                this.resolutionLevels[i] = new JPXCompositeResolutionLevel(this, i, codingStyle);
            }
        }

        public JPXResolutionLevel GetResolutionLevel(int resolutionLevelIndex) => 
            (resolutionLevelIndex != 0) ? ((JPXResolutionLevel) this.resolutionLevels[resolutionLevelIndex - 1]) : ((JPXResolutionLevel) this.baseResolutionLevel);

        public float[] Transform()
        {
            float[] numArray = new float[base.Width * base.Height];
            IJPXSubBandCoefficients lLSubBand = this.baseResolutionLevel.LLSubBand;
            JPXDiscreteWaveletTransformation transformation = this.codingStyle.UseWaveletTransformation ? ((JPXDiscreteWaveletTransformation) new JPXReversibleDiscreteWaveletTransformation(lLSubBand)) : ((JPXDiscreteWaveletTransformation) new JPXIrreversibleDiscreteWaveletTransformation(lLSubBand));
            if (this.resolutionLevels.Length == 0)
            {
                this.baseResolutionLevel.LLSubBand.FillCoefficients(numArray, 0);
            }
            else
            {
                JPXCompositeResolutionLevel level = this.resolutionLevels[0];
                transformation.Append(numArray, lLSubBand, level.HLSubBand, level.LHSubBand, level.HHSubBand);
                for (int i = 1; i < this.resolutionLevels.Length; i++)
                {
                    level = this.resolutionLevels[i];
                    transformation.Append(numArray, null, level.HLSubBand, level.LHSubBand, level.HHSubBand);
                }
            }
            return numArray;
        }

        public JPXCodingStyleComponent CodingStyle =>
            this.codingStyle;

        public int BitsPerComponent =>
            this.bitsPerComponent;

        public JPXQuantizationComponentParameters QuantizationParameters
        {
            get => 
                this.quantizationParameters;
            set => 
                this.quantizationParameters = value;
        }
    }
}

