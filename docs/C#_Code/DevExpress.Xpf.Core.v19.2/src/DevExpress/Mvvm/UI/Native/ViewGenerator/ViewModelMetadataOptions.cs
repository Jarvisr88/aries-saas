namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ViewModelMetadataOptions
    {
        private ViewModelMetadataOptions(bool scaffolding, IAttributesProvider attributesProvider, DevExpress.Mvvm.Native.LayoutType layoutType)
        {
            this.Scaffolding = scaffolding;
            this.AttributesProvider = attributesProvider;
            this.LayoutType = layoutType;
        }

        public static ViewModelMetadataOptions ForContextMenuRuntime() => 
            new ViewModelMetadataOptions(false, null, DevExpress.Mvvm.Native.LayoutType.ContextMenu);

        public static ViewModelMetadataOptions ForRuntime() => 
            new ViewModelMetadataOptions(false, null, DevExpress.Mvvm.Native.LayoutType.ToolBar);

        public static ViewModelMetadataOptions ForScaffolding(IAttributesProvider attributesProvider = null) => 
            new ViewModelMetadataOptions(true, attributesProvider, DevExpress.Mvvm.Native.LayoutType.ToolBar);

        public IAttributesProvider AttributesProvider { get; private set; }

        public bool Scaffolding { get; private set; }

        public DevExpress.Mvvm.Native.LayoutType LayoutType { get; private set; }
    }
}

