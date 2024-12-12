namespace System.ComponentModel
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class DXDescriptionAttribute : Attribute
    {
        private string description;

        public DXDescriptionAttribute() : this("")
        {
        }

        public DXDescriptionAttribute(string description)
        {
            this.description = description;
        }

        public string Description =>
            this.description;
    }
}

