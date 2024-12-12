namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class OfficeArtFileDrawingGroupRecord : OfficeArtPartBase
    {
        private readonly List<OfficeArtFileIdCluster> clusters = new List<OfficeArtFileIdCluster>();

        protected internal override int GetSize() => 
            0x10 + (this.Clusters.Count * 8);

        protected internal override void WriteCore(BinaryWriter writer)
        {
            writer.Write(this.MaxShapeId);
            int count = this.Clusters.Count;
            writer.Write((int) (count + 1));
            writer.Write(this.TotalShapes);
            writer.Write(this.TotalDrawings);
            for (int i = 0; i < count; i++)
            {
                this.Clusters[i].Write(writer);
            }
        }

        public override int HeaderInstanceInfo =>
            0;

        public override int HeaderTypeCode =>
            0xf006;

        public override int HeaderVersion =>
            0;

        public int MaxShapeId { get; set; }

        public int TotalShapes { get; set; }

        public int TotalDrawings { get; set; }

        public List<OfficeArtFileIdCluster> Clusters =>
            this.clusters;
    }
}

