namespace Devart.Data
{
    using System;
    using System.Drawing.Design;
    using System.Runtime.Serialization;

    [Serializable]
    internal class DataLinkToolboxItem : ToolboxItem
    {
        public DataLinkToolboxItem() : base(typeof(DataLink))
        {
        }

        public DataLinkToolboxItem(SerializationInfo info, StreamingContext context) : base(typeof(DataLink))
        {
            this.Deserialize(info, context);
        }
    }
}

