namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GenerateEditorOptions
    {
        internal GenerateEditorOptions(bool scaffolding, bool guessImageProperties, bool guessDisplayMembers, bool sortColumnsWithNegativeOrder, IEnumerable<TypeNamePropertyPair> collectionProperties, DevExpress.Mvvm.Native.LayoutType layoutType, bool skipIEnumerableProperties)
        {
            this.Scaffolding = scaffolding;
            this.GuessDisplayMembers = guessDisplayMembers;
            this.GuessImageProperties = guessImageProperties;
            this.CollectionProperties = collectionProperties;
            this.SortColumnsWithNegativeOrder = sortColumnsWithNegativeOrder;
            this.SkipIEnumerableProperties = skipIEnumerableProperties;
            this.LayoutType = layoutType;
        }

        public static GenerateEditorOptions ForGridRuntime() => 
            new GenerateEditorOptions(false, false, false, true, null, DevExpress.Mvvm.Native.LayoutType.Table, true);

        public static GenerateEditorOptions ForGridScaffolding(IEnumerable<TypeNamePropertyPair> collectionProperties = null) => 
            new GenerateEditorOptions(true, true, true, true, collectionProperties, DevExpress.Mvvm.Native.LayoutType.Table, true);

        public static GenerateEditorOptions ForLayoutRuntime() => 
            new GenerateEditorOptions(false, false, false, true, null, DevExpress.Mvvm.Native.LayoutType.DataForm, true);

        public static GenerateEditorOptions ForLayoutScaffolding(IEnumerable<TypeNamePropertyPair> collectionProperties = null) => 
            new GenerateEditorOptions(true, true, true, false, collectionProperties, DevExpress.Mvvm.Native.LayoutType.DataForm, true);

        public static GenerateEditorOptions ForRuntime() => 
            new GenerateEditorOptions(false, false, false, true, null, DevExpress.Mvvm.Native.LayoutType.Default, false);

        public bool Scaffolding { get; private set; }

        public bool GuessImageProperties { get; private set; }

        public bool GuessDisplayMembers { get; private set; }

        public bool SortColumnsWithNegativeOrder { get; private set; }

        public bool SkipIEnumerableProperties { get; private set; }

        public IEnumerable<TypeNamePropertyPair> CollectionProperties { get; private set; }

        internal DevExpress.Mvvm.Native.LayoutType LayoutType { get; private set; }
    }
}

