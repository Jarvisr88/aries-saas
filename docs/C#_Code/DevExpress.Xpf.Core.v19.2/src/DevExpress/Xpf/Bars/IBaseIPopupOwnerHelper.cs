namespace DevExpress.Xpf.Bars
{
    using System;

    public interface IBaseIPopupOwnerHelper
    {
        void ClosePopup();
        void ClosePopup(bool ignoreSetMenuMode);
        bool GetActAsDropDown();
        void OnShowContentInArrowChanged();
        void ShowPopup();
    }
}

