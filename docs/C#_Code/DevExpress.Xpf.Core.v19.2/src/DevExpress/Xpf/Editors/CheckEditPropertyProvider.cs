namespace DevExpress.Xpf.Editors
{
    using System;

    public class CheckEditPropertyProvider : ActualPropertyProvider
    {
        public CheckEditPropertyProvider(CheckEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;
    }
}

