namespace DevExpress.Xpf.Bars
{
    using System;

    public enum PopupItemClickBehaviour
    {
        public const PopupItemClickBehaviour None = PopupItemClickBehaviour.None;,
        public const PopupItemClickBehaviour ClosePopup = PopupItemClickBehaviour.ClosePopup;,
        public const PopupItemClickBehaviour CloseChildren = PopupItemClickBehaviour.CloseChildren;,
        public const PopupItemClickBehaviour CloseCurrentBranch = PopupItemClickBehaviour.CloseCurrentBranch;,
        public const PopupItemClickBehaviour CloseAllPopups = PopupItemClickBehaviour.CloseAllPopups;,
        public const PopupItemClickBehaviour Undefined = PopupItemClickBehaviour.Undefined;
    }
}

