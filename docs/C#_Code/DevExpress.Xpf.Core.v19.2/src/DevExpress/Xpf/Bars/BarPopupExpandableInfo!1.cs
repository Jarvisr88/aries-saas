namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public abstract class BarPopupExpandableInfo : PopupInfo<TPopup>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ExpandModeProperty;

        static BarPopupExpandableInfo();
        protected BarPopupExpandableInfo();
        private static DependencyProperty AddOwnerInternal(DependencyProperty prop, Type popupType);
    }
}

