namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StickyNotesEdit : BaseEdit
    {
        public static readonly DependencyProperty TitleProperty;

        static StickyNotesEdit()
        {
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(StickyNotesEdit), typeof(StickyNotesEditSettings), (CreateEditorMethod) (() => new StickyNotesEdit()), (CreateEditorSettingsMethod) (() => new StickyNotesEditSettings()));
            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(StickyNotesEdit), new PropertyMetadata(string.Empty));
        }

        protected override EditStrategyBase CreateEditStrategy() => 
            new StickyNotesEditStrategy(this);

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StickyNotesEdit.<>c <>9 = new StickyNotesEdit.<>c();

            internal IBaseEdit <.cctor>b__1_0() => 
                new StickyNotesEdit();

            internal BaseEditSettings <.cctor>b__1_1() => 
                new StickyNotesEditSettings();
        }
    }
}

