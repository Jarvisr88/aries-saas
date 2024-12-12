namespace DevExpress.XtraPrinting.Drawing
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing.Printing;

    public class MarginsFloat : MarginsFBase
    {
        public MarginsFloat()
        {
        }

        public MarginsFloat(Margins margins) : base((float) margins.Left, (float) margins.Right, (float) margins.Top, (float) margins.Bottom)
        {
        }

        public MarginsFloat(float left, float right, float top, float bottom) : base(left, right, top, bottom)
        {
        }

        public override object Clone() => 
            new MarginsFloat(base.Left, base.Right, base.Top, base.Bottom);

        public static implicit operator MarginsFloat(MarginsF margin) => 
            new MarginsFloat(margin.Left, margin.Right, margin.Top, margin.Bottom);
    }
}

