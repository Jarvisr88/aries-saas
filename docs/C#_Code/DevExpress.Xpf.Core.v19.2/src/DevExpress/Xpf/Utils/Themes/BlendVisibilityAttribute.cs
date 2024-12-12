namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Runtime.CompilerServices;

    public class BlendVisibilityAttribute : Attribute
    {
        public BlendVisibilityAttribute() : this(true)
        {
        }

        public BlendVisibilityAttribute(bool isVisible)
        {
            this.IsVisible = isVisible;
        }

        public bool IsVisible { get; set; }
    }
}

