namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;

    public class DateRangeEditSettings : PopupBaseEditSettings
    {
        static DateRangeEditSettings()
        {
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(DateRangeEdit), typeof(DateRangeEditSettings), (CreateEditorMethod) (() => new DateRangeEdit()), (CreateEditorSettingsMethod) (() => new DateRangeEditSettings()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateRangeEditSettings.<>c <>9 = new DateRangeEditSettings.<>c();

            internal IBaseEdit <.cctor>b__0_0() => 
                new DateRangeEdit();

            internal BaseEditSettings <.cctor>b__0_1() => 
                new DateRangeEditSettings();
        }
    }
}

