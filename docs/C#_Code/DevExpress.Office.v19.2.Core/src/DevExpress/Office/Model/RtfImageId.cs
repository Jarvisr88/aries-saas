namespace DevExpress.Office.Model
{
    using System;

    public class RtfImageId : IUniqueImageId
    {
        private readonly int id;

        public RtfImageId(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            RtfImageId id = obj as RtfImageId;
            return ((id != null) ? (this.id == id.id) : false);
        }

        public override int GetHashCode() => 
            this.id;
    }
}

