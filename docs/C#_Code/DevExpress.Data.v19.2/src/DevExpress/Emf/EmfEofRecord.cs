namespace DevExpress.Emf
{
    using System;

    public class EmfEofRecord : EmfRecord
    {
        public const int RecordSize = 20;

        public override void Write(EmfContentWriter writer)
        {
            writer.Write(14);
            writer.Write(20);
            writer.Write(new byte[8]);
            writer.Write(20);
        }
    }
}

