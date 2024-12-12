namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class StickyNotesEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(StickyNotesEditSettings), new PropertyMetadata(string.Empty));

        static StickyNotesEditSettings()
        {
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(StickyNotesEdit), typeof(StickyNotesEditSettings), (CreateEditorMethod) (() => new StickyNotesEdit()), (CreateEditorSettingsMethod) (() => new StickyNotesEditSettings()));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            StickyNotesEdit stickyNotesEdit = edit as StickyNotesEdit;
            if (stickyNotesEdit != null)
            {
                base.SetValueFromSettings(TitleProperty, () => stickyNotesEdit.Title = this.Title);
            }
        }

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
            public static readonly StickyNotesEditSettings.<>c <>9 = new StickyNotesEditSettings.<>c();

            internal IBaseEdit <.cctor>b__4_0() => 
                new StickyNotesEdit();

            internal BaseEditSettings <.cctor>b__4_1() => 
                new StickyNotesEditSettings();
        }
    }
}

