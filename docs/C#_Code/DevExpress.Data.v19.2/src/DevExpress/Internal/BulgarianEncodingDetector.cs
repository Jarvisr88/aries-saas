namespace DevExpress.Internal
{
    using System;

    public abstract class BulgarianEncodingDetector : SingleByteEncodingDetector
    {
        private static readonly int[] precedenceMatrix = LoadPrecedenceTable("DevExpress.Data.Utils.Text.precedence_bulgarian.bin");

        protected BulgarianEncodingDetector()
        {
        }

        protected internal override float PositiveRatio =>
            0.969392f;

        protected internal override int[] PrecedenceMatrix =>
            precedenceMatrix;
    }
}

