namespace DevExpress.Internal
{
    using System;

    public abstract class GreekEncodingDetector : SingleByteEncodingDetector
    {
        private static readonly int[] precedenceMatrix = LoadPrecedenceTable("DevExpress.Data.Utils.Text.precedence_greek.bin");

        protected GreekEncodingDetector()
        {
        }

        protected internal override float PositiveRatio =>
            0.982851f;

        protected internal override int[] PrecedenceMatrix =>
            precedenceMatrix;
    }
}

