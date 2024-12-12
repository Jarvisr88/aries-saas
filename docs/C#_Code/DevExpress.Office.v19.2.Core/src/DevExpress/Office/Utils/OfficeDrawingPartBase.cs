namespace DevExpress.Office.Utils
{
    using System;
    using System.IO;

    public abstract class OfficeDrawingPartBase
    {
        protected OfficeDrawingPartBase()
        {
        }

        protected internal abstract int GetSize();
        protected internal virtual bool ShouldWrite() => 
            true;

        public void Write(BinaryWriter writer)
        {
            if (this.ShouldWrite())
            {
                this.WriteHeader(writer);
                this.WriteCore(writer);
            }
        }

        protected internal virtual void WriteCore(BinaryWriter writer)
        {
        }

        protected internal void WriteHeader(BinaryWriter writer)
        {
            new OfficeArtRecordHeader { 
                InstanceInfo = this.HeaderInstanceInfo,
                Length = this.Length,
                TypeCode = this.HeaderTypeCode,
                Version = this.HeaderVersion
            }.Write(writer);
        }

        public abstract int HeaderVersion { get; }

        public abstract int HeaderInstanceInfo { get; }

        public abstract int HeaderTypeCode { get; }

        public virtual int Length =>
            this.GetSize();
    }
}

