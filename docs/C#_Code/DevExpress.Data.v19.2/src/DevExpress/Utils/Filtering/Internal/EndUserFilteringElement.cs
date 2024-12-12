namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class EndUserFilteringElement : StatelessObject, IEndUserFilteringElement
    {
        private readonly string pathCore;

        public EndUserFilteringElement(Func<IServiceProvider> getServiceProvider, string path);
        protected sealed override string GetId();

        public string Path { get; }

        public string Caption { get; }

        public string Description { get; }

        public string Layout { get; }

        public int Order { get; }

        public bool ApplyFormatInEditMode { get; }

        public string DataFormatString { get; }

        public string NullDisplayText { get; }

        public System.ComponentModel.DataAnnotations.DataType? DataType { get; }

        public Type EnumDataType { get; }

        public bool IsVisible { get; }

        public bool IsEnabled { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EndUserFilteringElement.<>c <>9;
            public static Func<IMetadataProvider, Func<string, string>> <>9__6_0;
            public static Func<IMetadataProvider, Func<string, string>> <>9__8_0;
            public static Func<IMetadataProvider, Func<string, string>> <>9__10_0;
            public static Func<IMetadataProvider, Func<string, int>> <>9__12_0;
            public static Func<IMetadataProvider, Func<string, bool>> <>9__14_0;
            public static Func<IMetadataProvider, Func<string, string>> <>9__16_0;
            public static Func<IMetadataProvider, Func<string, string>> <>9__18_0;
            public static Func<IMetadataProvider, Func<string, DataType?>> <>9__20_0;
            public static Func<IMetadataProvider, Func<string, Type>> <>9__22_0;
            public static Func<IMetadataProvider, Func<string, bool>> <>9__24_0;
            public static Func<IBehaviorProvider, Func<string, bool>> <>9__24_1;
            public static Func<IBehaviorProvider, Func<string, bool>> <>9__26_0;

            static <>c();
            internal Func<string, bool> <get_ApplyFormatInEditMode>b__14_0(IMetadataProvider x);
            internal Func<string, string> <get_Caption>b__6_0(IMetadataProvider x);
            internal Func<string, string> <get_DataFormatString>b__16_0(IMetadataProvider x);
            internal Func<string, DataType?> <get_DataType>b__20_0(IMetadataProvider x);
            internal Func<string, string> <get_Description>b__8_0(IMetadataProvider x);
            internal Func<string, Type> <get_EnumDataType>b__22_0(IMetadataProvider x);
            internal Func<string, bool> <get_IsEnabled>b__26_0(IBehaviorProvider x);
            internal Func<string, bool> <get_IsVisible>b__24_0(IMetadataProvider x);
            internal Func<string, bool> <get_IsVisible>b__24_1(IBehaviorProvider x);
            internal Func<string, string> <get_Layout>b__10_0(IMetadataProvider x);
            internal Func<string, string> <get_NullDisplayText>b__18_0(IMetadataProvider x);
            internal Func<string, int> <get_Order>b__12_0(IMetadataProvider x);
        }
    }
}

