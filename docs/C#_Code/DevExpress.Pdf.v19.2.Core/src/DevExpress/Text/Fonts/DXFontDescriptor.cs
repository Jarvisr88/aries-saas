namespace DevExpress.Text.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DXFontDescriptor : IEquatable<DXFontDescriptor>
    {
        public string FamilyName { get; }
        public DXFontStyle Style { get; }
        public DXFontStretch Stretch { get; }
        public DXFontWeight Weight { get; }
        public DXFontDescriptor(string familyName) : this(familyName, DXFontWeight.Normal, DXFontStyle.Regular, DXFontStretch.Normal)
        {
        }

        public DXFontDescriptor(string familyName, DXFontWeight weight, DXFontStyle style, DXFontStretch stretch)
        {
            this.<FamilyName>k__BackingField = familyName;
            this.<Weight>k__BackingField = weight;
            this.<Style>k__BackingField = style;
            this.<Stretch>k__BackingField = stretch;
        }

        public bool Equals(DXFontDescriptor descriptor) => 
            (this.FamilyName == descriptor.FamilyName) && ((this.Style == descriptor.Style) && ((this.Stretch == descriptor.Stretch) && (this.Weight == descriptor.Weight)));

        public override int GetHashCode() => 
            (((((((-1674725189 * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.FamilyName)) * -1521134295) + this.Style.GetHashCode()) * -1521134295) + this.Stretch.GetHashCode()) * -1521134295) + this.Weight.GetHashCode();
    }
}

