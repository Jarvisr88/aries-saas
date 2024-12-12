namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class TextEditStyleSettings : BaseEditStyleSettings
    {
        protected internal override Inline CreateInlineForHighlighting(FrameworkElement editor, string text)
        {
            Inline inline = base.CreateInlineForHighlighting(editor, text);
            Func<TextEdit, TextDecorationCollection> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<TextEdit, TextDecorationCollection> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.TextDecorations;
            }
            inline.TextDecorations = (editor as TextEdit).Return<TextEdit, TextDecorationCollection>(evaluator, <>c.<>9__2_1 ??= ((Func<TextDecorationCollection>) (() => null)));
            return inline;
        }

        private bool ShouldSerializeHighlightedTextBackground() => 
            !ReferenceEquals(base.HighlightedTextBackground, Brushes.Yellow);

        private bool ShouldSerializeHighlightedTextForeground() => 
            !ReferenceEquals(base.HighlightedTextForeground, Brushes.Black);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEditStyleSettings.<>c <>9 = new TextEditStyleSettings.<>c();
            public static Func<TextEdit, TextDecorationCollection> <>9__2_0;
            public static Func<TextDecorationCollection> <>9__2_1;

            internal TextDecorationCollection <CreateInlineForHighlighting>b__2_0(TextEdit x) => 
                x.TextDecorations;

            internal TextDecorationCollection <CreateInlineForHighlighting>b__2_1() => 
                null;
        }
    }
}

