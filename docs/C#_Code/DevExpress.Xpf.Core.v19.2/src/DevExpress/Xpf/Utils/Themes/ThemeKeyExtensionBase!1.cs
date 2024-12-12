namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ThemeKeyExtensionBase<T> : ThemeKeyExtensionInternalBase<T>
    {
        protected ThemeKeyExtensionBase()
        {
            this.IsThemeIndependent = false;
        }

        protected override bool IsSameTheme(ThemeKeyExtensionGeneric other)
        {
            ThemeKeyExtensionBase<T> base2 = (ThemeKeyExtensionBase<T>) other;
            return (((this.IsThemeIndependent || base2.IsThemeIndependent) || (base.IsSameTheme(other) && Equals(this.Assembly, base2.Assembly))) && Equals(base.ResourceKeyCore, base2.ResourceKeyCore));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsThemeIndependent { get; set; }
    }
}

