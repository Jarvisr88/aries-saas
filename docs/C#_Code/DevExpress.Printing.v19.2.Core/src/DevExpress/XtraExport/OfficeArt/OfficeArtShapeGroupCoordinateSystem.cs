namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtShapeGroupCoordinateSystem : OfficeArtPartBase
    {
        protected internal override int GetSize() => 
            0x10;

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.Left);
            writer.Write(this.Top);
            writer.Write(this.Right);
            writer.Write(this.Bottom);
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf009;

        public override int HeaderVersion =>
            1;

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }
    }
}

