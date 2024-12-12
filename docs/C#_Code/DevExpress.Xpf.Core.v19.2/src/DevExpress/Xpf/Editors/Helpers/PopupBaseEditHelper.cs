namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Popups;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public static class PopupBaseEditHelper
    {
        private static Button FindButton(PopupBaseEdit edit, string name) => 
            (Button) LayoutHelper.FindElement((FrameworkElement) edit.Popup.Child, e => (e is Button) && (e.Name == name));

        public static Button GetCancelButton(this PopupBaseEdit edit) => 
            FindButton(edit, "PART_CancelButton");

        public static Button GetCloseButton(this PopupBaseEdit edit) => 
            FindButton(edit, "PART_CloseButton");

        public static ContentControl GetFooter(this PopupBaseEdit edit)
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = e => (e is ContentControl) && (e.Name == "PART_Footer");
            }
            return (ContentControl) LayoutHelper.FindElement((FrameworkElement) edit.Popup.Child, predicate);
        }

        public static Border GetFooterLayer(this PopupBaseEdit edit)
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__8_0;
                predicate = <>c.<>9__8_0 = e => (e is Border) && (e.Name == "PART_FooterLayer");
            }
            return (Border) LayoutHelper.FindElement((FrameworkElement) edit.Popup.Child, predicate);
        }

        public static bool GetIsValueChangedViaPopup(BaseEditSettings edit)
        {
            Func<PopupBaseEditSettings, bool> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<PopupBaseEditSettings, bool> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.IsValueChangedViaPopup;
            }
            return (edit as PopupBaseEditSettings).Return<PopupBaseEditSettings, bool>(evaluator, (<>c.<>9__0_1 ??= () => false));
        }

        public static Button GetOkButton(this PopupBaseEdit edit) => 
            FindButton(edit, "PART_OkButton");

        public static EditorPopupBase GetPopup(this PopupBaseEdit edit) => 
            edit.Popup;

        public static PopupSizeGrip GetSizeGrip(this PopupBaseEdit edit)
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__6_0;
                predicate = <>c.<>9__6_0 = e => e is PopupSizeGrip;
            }
            return (PopupSizeGrip) LayoutHelper.FindElement((FrameworkElement) edit.Popup.Child, predicate);
        }

        public static ContentControl GetTop(this PopupBaseEdit edit)
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = e => (e is ContentControl) && (e.Name == "PART_Top");
            }
            return (ContentControl) LayoutHelper.FindElement((FrameworkElement) edit.Popup.Child, predicate);
        }

        public static void SetIsValueChangedViaPopup(PopupBaseEditSettings edit, bool value)
        {
            if (edit != null)
            {
                edit.IsValueChangedViaPopup = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupBaseEditHelper.<>c <>9 = new PopupBaseEditHelper.<>c();
            public static Func<PopupBaseEditSettings, bool> <>9__0_0;
            public static Func<bool> <>9__0_1;
            public static Predicate<FrameworkElement> <>9__6_0;
            public static Predicate<FrameworkElement> <>9__7_0;
            public static Predicate<FrameworkElement> <>9__8_0;
            public static Predicate<FrameworkElement> <>9__9_0;

            internal bool <GetFooter>b__7_0(FrameworkElement e) => 
                (e is ContentControl) && (e.Name == "PART_Footer");

            internal bool <GetFooterLayer>b__8_0(FrameworkElement e) => 
                (e is Border) && (e.Name == "PART_FooterLayer");

            internal bool <GetIsValueChangedViaPopup>b__0_0(PopupBaseEditSettings x) => 
                x.IsValueChangedViaPopup;

            internal bool <GetIsValueChangedViaPopup>b__0_1() => 
                false;

            internal bool <GetSizeGrip>b__6_0(FrameworkElement e) => 
                e is PopupSizeGrip;

            internal bool <GetTop>b__9_0(FrameworkElement e) => 
                (e is ContentControl) && (e.Name == "PART_Top");
        }
    }
}

