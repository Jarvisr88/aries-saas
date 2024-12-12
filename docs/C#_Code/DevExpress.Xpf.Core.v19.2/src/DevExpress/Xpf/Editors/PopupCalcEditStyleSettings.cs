namespace DevExpress.Xpf.Editors
{
    public class PopupCalcEditStyleSettings : PopupBaseEditStyleSettings
    {
        protected internal override PopupCloseMode GetClosePopupOnClickMode(PopupBaseEdit editor) => 
            PopupCloseMode.Normal;
    }
}

