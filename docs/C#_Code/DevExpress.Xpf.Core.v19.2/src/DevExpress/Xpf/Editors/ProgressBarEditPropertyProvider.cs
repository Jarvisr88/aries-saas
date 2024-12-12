namespace DevExpress.Xpf.Editors
{
    using System;

    public class ProgressBarEditPropertyProvider : ActualPropertyProvider
    {
        public ProgressBarEditPropertyProvider(ProgressBarEdit editor) : base(editor)
        {
        }

        public override bool CalcSuppressFeatures() => 
            false;
    }
}

