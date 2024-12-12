namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class VmlFocusSize : ISupportsCopyFrom<VmlFocusSize>
    {
        public void CopyFrom(VmlFocusSize source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.Width = source.Width;
            this.Height = source.Width;
        }

        public bool IsDefault() => 
            (this.Width == 0f) && (this.Height == 0f);

        public override string ToString()
        {
            string str2 = ((int) Math.Round((double) (this.Height * 100f))).ToString(CultureInfo.InvariantCulture) + "%";
            return ((((int) Math.Round((double) (this.Width * 100f))).ToString(CultureInfo.InvariantCulture) + "%") + "," + str2);
        }

        public float Width { get; set; }

        public float Height { get; set; }
    }
}

