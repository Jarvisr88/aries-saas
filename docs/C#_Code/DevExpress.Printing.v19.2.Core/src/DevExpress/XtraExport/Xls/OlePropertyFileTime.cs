namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class OlePropertyFileTime : OlePropertyBase
    {
        private long fileTime;

        public OlePropertyFileTime(int propertyId) : base(propertyId, 0x40)
        {
        }

        public override void Execute(IDocumentPropertiesContainer propertiesContainer, OlePropertySetBase propertySet)
        {
            string propertyName = propertySet.GetPropertyName(base.PropertyId);
            if (!string.IsNullOrEmpty(propertyName) && this.IsValidDateTime)
            {
                propertiesContainer.SetDateTime(propertyName, this.Value);
            }
        }

        public override int GetSize(OlePropertySetBase propertySet) => 
            12;

        public override void Read(BinaryReader reader, OlePropertySetBase propertySet)
        {
            this.fileTime = reader.ReadInt64();
        }

        public override void Write(BinaryWriter writer, OlePropertySetBase propertySet)
        {
            writer.Write(base.PropertyType);
            writer.Write(this.fileTime);
        }

        public DateTime Value
        {
            get
            {
                if (this.fileTime < 0L)
                {
                    return DateTime.MinValue;
                }
                try
                {
                    return DateTime.FromFileTime(this.fileTime);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return DateTime.MaxValue;
                }
            }
            set
            {
                try
                {
                    this.fileTime = value.ToFileTime();
                }
                catch (ArgumentOutOfRangeException)
                {
                    this.fileTime = 0L;
                }
            }
        }

        public bool IsValidDateTime
        {
            get
            {
                try
                {
                    DateTime.FromFileTime(this.fileTime);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }
                return true;
            }
        }
    }
}

