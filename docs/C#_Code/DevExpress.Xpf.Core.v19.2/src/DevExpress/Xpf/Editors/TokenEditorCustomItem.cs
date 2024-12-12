namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TokenEditorCustomItem : CustomItem
    {
        public TokenEditorCustomItem()
        {
            this.EditableTokens = new List<int>();
        }

        public bool UseTokenDisplayText { get; set; }

        public int EditableTokenIndex
        {
            get
            {
                Func<List<int>, int> evaluator = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<List<int>, int> local1 = <>c.<>9__6_0;
                    evaluator = <>c.<>9__6_0 = x => (x.Count > 0) ? x[0] : -1;
                }
                return this.EditableTokens.Return<List<int>, int>(evaluator, (<>c.<>9__6_1 ??= () => -1));
            }
        }

        public List<int> EditableTokens { get; set; }

        public string NullText { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TokenEditorCustomItem.<>c <>9 = new TokenEditorCustomItem.<>c();
            public static Func<List<int>, int> <>9__6_0;
            public static Func<int> <>9__6_1;

            internal int <get_EditableTokenIndex>b__6_0(List<int> x) => 
                (x.Count > 0) ? x[0] : -1;

            internal int <get_EditableTokenIndex>b__6_1() => 
                -1;
        }
    }
}

