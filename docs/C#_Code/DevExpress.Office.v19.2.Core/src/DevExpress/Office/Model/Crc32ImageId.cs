namespace DevExpress.Office.Model
{
    using System;

    public class Crc32ImageId : IUniqueImageId
    {
        private readonly int crc32;

        public Crc32ImageId(int crc32)
        {
            this.crc32 = crc32;
        }

        public override bool Equals(object obj)
        {
            Crc32ImageId id = obj as Crc32ImageId;
            return ((id != null) ? (this.crc32 == id.crc32) : false);
        }

        public override int GetHashCode() => 
            this.crc32;
    }
}

