namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Controls;

    public class ButtonContainer : ContentPresenter
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Info != null)
            {
                this.Info.FindContent(this);
            }
        }

        protected internal ButtonInfoBase Info =>
            base.DataContext as ButtonInfoBase;
    }
}

