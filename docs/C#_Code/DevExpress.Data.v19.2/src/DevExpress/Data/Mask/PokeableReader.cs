namespace DevExpress.Data.Mask
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class PokeableReader
    {
        private List<TextReader> readers;

        public PokeableReader();
        public PokeableReader(TextReader firstReader);
        public int Peek();
        public void Poke(string nextInput);
        public void PokeReader(TextReader reader);
        public int Read();
    }
}

