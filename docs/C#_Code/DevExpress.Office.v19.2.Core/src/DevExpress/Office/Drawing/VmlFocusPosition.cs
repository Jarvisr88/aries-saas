namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class VmlFocusPosition : ISupportsCopyFrom<VmlFocusPosition>
    {
        public void CopyFrom(VmlFocusPosition source)
        {
            Guard.ArgumentNotNull(source, "source");
            this.Left = source.Left;
            this.Top = source.Top;
        }

        public bool IsDefault() => 
            (this.Left == 0f) && (this.Top == 0f);

        public override string ToString()
        {
            string str2 = ((int) Math.Round((double) (this.Top * 100f))).ToString(CultureInfo.InvariantCulture) + "%";
            return ((((int) Math.Round((double) (this.Left * 100f))).ToString(CultureInfo.InvariantCulture) + "%") + "," + str2);
        }

        public float Left { get; set; }

        public float Top { get; set; }
    }
}

