namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BandBricksPair
    {
        public BandBricksPair(DocumentBand band, IList<Brick> bricks);

        public DocumentBand Band { get; private set; }

        public IList<Brick> Bricks { get; private set; }
    }
}

