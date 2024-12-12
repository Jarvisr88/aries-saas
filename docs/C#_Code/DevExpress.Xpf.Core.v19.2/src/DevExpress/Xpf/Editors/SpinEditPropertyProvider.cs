namespace DevExpress.Xpf.Editors
{
    using System;

    public class SpinEditPropertyProvider : ButtonEditPropertyProvider
    {
        public SpinEditPropertyProvider(SpinEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            base.CalcSuppressFeatures() && ((this.Editor.MinValue == null) && (this.Editor.MaxValue == null));

        private SpinEdit Editor =>
            base.Editor as SpinEdit;
    }
}

