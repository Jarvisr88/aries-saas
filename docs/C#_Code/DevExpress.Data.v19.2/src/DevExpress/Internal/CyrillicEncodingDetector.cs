namespace DevExpress.Internal
{
    using System;

    public abstract class CyrillicEncodingDetector : SingleByteEncodingDetector
    {
        private static readonly int[] precedenceMatrix = LoadPrecedenceTable("DevExpress.Data.Utils.Text.precedence_cyrillic.bin");

        protected CyrillicEncodingDetector()
        {
        }

        protected internal override float PositiveRatio =>
            0.976601f;

        protected internal override int[] PrecedenceMatrix =>
            precedenceMatrix;
    }
}

