namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public class SerializationInfo
    {
        private int index;
        private bool serialized;

        public SerializationInfo(int index)
        {
            this.index = index;
        }

        public int Index =>
            this.index;

        public bool Serialized
        {
            get => 
                this.serialized;
            set => 
                this.serialized = value;
        }
    }
}

