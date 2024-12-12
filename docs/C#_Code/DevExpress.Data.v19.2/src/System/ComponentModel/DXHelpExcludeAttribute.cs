namespace System.ComponentModel
{
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class DXHelpExcludeAttribute : Attribute
    {
        private bool exclude;

        public DXHelpExcludeAttribute(bool exclude)
        {
            this.exclude = exclude;
        }

        public bool Exclude =>
            this.exclude;
    }
}

