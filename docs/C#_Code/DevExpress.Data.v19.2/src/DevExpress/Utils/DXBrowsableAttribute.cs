namespace DevExpress.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class DXBrowsableAttribute : Attribute
    {
        private readonly bool browsable;

        public DXBrowsableAttribute() : this(true)
        {
        }

        public DXBrowsableAttribute(bool browsable)
        {
            this.browsable = browsable;
        }

        public bool Browsable =>
            this.browsable;
    }
}

