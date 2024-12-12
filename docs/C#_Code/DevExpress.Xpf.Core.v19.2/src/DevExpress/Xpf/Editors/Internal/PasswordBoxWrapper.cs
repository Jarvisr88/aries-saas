namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class PasswordBoxWrapper : EditBoxWrapper
    {
        private static readonly Func<object, object> GetTextEditorHandler;
        private static readonly Action<object> PasteHandler;

        static PasswordBoxWrapper()
        {
            Type type = typeof(Application);
            Type instanceType = type.Assembly.GetType("System.Windows.Documents.TextEditor");
            int? parametersCount = null;
            GetTextEditorHandler = (Func<object, object>) ReflectionHelper.CreateInstanceMethodHandler(null, "_GetTextEditor", BindingFlags.NonPublic | BindingFlags.Static, instanceType, parametersCount, null, true);
            Type type3 = type.Assembly.GetType("System.Windows.Documents.TextEditorCopyPaste");
            parametersCount = null;
            PasteHandler = ReflectionHelper.CreateInstanceMethodHandler<Action<object>>(null, "Paste", BindingFlags.NonPublic | BindingFlags.Static, type3, parametersCount, null, true);
        }

        public PasswordBoxWrapper(PasswordBoxEdit editor) : base(editor)
        {
        }

        public override void Copy()
        {
        }

        public override void Cut()
        {
        }

        public override int GetCharacterIndexFromLineIndex(int lineIndex) => 
            -1;

        public override int GetCharacterIndexFromPoint(Point point, bool snapToText) => 
            -1;

        public override int GetFirstVisibleLineIndex() => 
            -1;

        public override int GetLastVisibleLineIndex() => 
            -1;

        public override int GetLineIndexFromCharacterIndex(int charIndex) => 
            -1;

        public override int GetLineLength(int lineIndex) => 
            -1;

        public override string GetLineText(int lineIndex)
        {
            PasswordBox editBox = this.EditBox;
            if (editBox != null)
            {
                return editBox.Password;
            }
            PasswordBox local1 = editBox;
            return null;
        }

        public override bool NeedsKey(Key key, ModifierKeys modifiers) => 
            (key != Key.Up) && ((key != Key.Down) && (((key == Key.Left) || (key == Key.Right)) ? this.Editor.IsValueChanged : true));

        public override void Paste()
        {
            if (this.EditBox != null)
            {
                PasteHandler(GetTextEditorHandler(this.EditBox));
            }
        }

        public override void ScrollToHome()
        {
        }

        public override void Select(int start, int length)
        {
            if (start == 0)
            {
                Func<string, int> evaluator = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<string, int> local1 = <>c.<>9__51_0;
                    evaluator = <>c.<>9__51_0 = x => x.Length;
                }
                if (length == this.Text.Return<string, int>(evaluator, (<>c.<>9__51_1 ??= () => 0)))
                {
                    this.SelectAll();
                }
            }
        }

        public override void SelectAll()
        {
            PasswordBox editBox = this.EditBox;
            if (editBox == null)
            {
                PasswordBox local1 = editBox;
            }
            else
            {
                editBox.SelectAll();
            }
        }

        public override void Undo()
        {
        }

        public override void UnselectAll()
        {
        }

        private PasswordBoxEdit Editor =>
            base.Editor as PasswordBoxEdit;

        private PasswordBox EditBox =>
            this.Editor.PasswordBox;

        public override int LineCount =>
            1;

        public override bool CanUndo =>
            false;

        public override bool IsUndoEnabled
        {
            get => 
                false;
            set
            {
            }
        }

        public override System.Windows.Controls.CharacterCasing CharacterCasing
        {
            get => 
                System.Windows.Controls.CharacterCasing.Normal;
            set
            {
            }
        }

        public override string Text =>
            (this.EditBox != null) ? this.EditBox.Password : string.Empty;

        public override object EditValue
        {
            get => 
                (this.EditBox != null) ? this.EditBox.Password : string.Empty;
            set
            {
                if ((this.EditBox != null) && (this.EditBox.Password != ((string) value)))
                {
                    this.EditBox.Password = (string) value;
                }
            }
        }

        public override int MaxLength
        {
            get => 
                (this.EditBox == null) ? -1 : this.EditBox.MaxLength;
            set
            {
                if (this.EditBox != null)
                {
                    this.EditBox.MaxLength = value;
                }
            }
        }

        public override Brush Foreground
        {
            get
            {
                PasswordBox editBox = this.EditBox;
                if (editBox != null)
                {
                    return editBox.Foreground;
                }
                PasswordBox local1 = editBox;
                return null;
            }
        }

        public override bool IsReadOnly { get; set; }

        public override string SelectedText
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        public override int SelectionLength
        {
            get => 
                -1;
            set
            {
            }
        }

        public override int SelectionStart
        {
            get => 
                -1;
            set
            {
            }
        }

        public override int CaretIndex
        {
            get => 
                -1;
            set
            {
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PasswordBoxWrapper.<>c <>9 = new PasswordBoxWrapper.<>c();
            public static Func<string, int> <>9__51_0;
            public static Func<int> <>9__51_1;

            internal int <Select>b__51_0(string x) => 
                x.Length;

            internal int <Select>b__51_1() => 
                0;
        }
    }
}

