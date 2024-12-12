namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtFileIdCluster
    {
        public const int Size = 8;

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.ClusterId);
            writer.Write(this.LargestShapeId);
        }

        public int ClusterId { get; set; }

        public int LargestShapeId { get; set; }
    }
}

