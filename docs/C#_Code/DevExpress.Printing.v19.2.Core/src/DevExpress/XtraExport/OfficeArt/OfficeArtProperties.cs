namespace DevExpress.XtraExport.OfficeArt
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class OfficeArtProperties : OfficeArtPartBase
    {
        private readonly List<IOfficeArtProperty> properties = new List<IOfficeArtProperty>();

        protected internal override int GetSize()
        {
            int num = 0;
            foreach (IOfficeArtProperty property in this.Properties)
            {
                num += property.Size;
            }
            return num;
        }

        protected internal override void WriteCore(BinaryWriter writer)
        {
            foreach (IOfficeArtProperty property in this.Properties)
            {
                property.Write(writer);
            }
            foreach (IOfficeArtProperty property2 in this.Properties)
            {
                if (property2.Complex)
                {
                    writer.Write(property2.ComplexData);
                }
            }
        }

        public override int HeaderInstanceInfo =>
            this.Properties.Count;

        public override int HeaderTypeCode =>
            0xf00b;

        public override int HeaderVersion =>
            3;

        protected internal List<IOfficeArtProperty> Properties =>
            this.properties;
    }
}

