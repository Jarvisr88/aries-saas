namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXPacketPosition
    {
        private readonly int component;
        private readonly int layer;
        private readonly int resolutionLevel;
        private readonly int precinct;

        public JPXPacketPosition(int component, int layer, int resolutionLevel, int precinct)
        {
            this.component = component;
            this.layer = layer;
            this.resolutionLevel = resolutionLevel;
            this.precinct = precinct;
        }

        public int Component =>
            this.component;

        public int Layer =>
            this.layer;

        public int ResolutionLevel =>
            this.resolutionLevel;

        public int Precinct =>
            this.precinct;
    }
}

