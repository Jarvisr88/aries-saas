namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class MergingProperties
    {
        public static readonly DependencyProperty ToolBarMergeStyleProperty;
        [Obsolete("Use the ElementMergingBehavior property instead")]
        public static readonly DependencyProperty AllowMergingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty AllowMergingInternalProperty;
        public static readonly DependencyProperty NameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty ForceElementMergingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty OverridesAllowMergingBehaviorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BlockMergingRegionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty MergingRegionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty AllowMergingCoreProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty BlockMergingRegionIDCoreProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty RegionIDCoreProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty BlockMergingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty BlockMergingCoreProperty;
        public static readonly DependencyProperty ElementMergingBehaviorProperty;
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static readonly DependencyProperty HideElementsProperty;

        static MergingProperties();
        internal static bool CheckRegions(object first, object second);
        private static object CoerceAllowMergingCoreProperty(DependencyObject d, object baseValue);
        private static object CoerceBlockMergingCore(DependencyObject d, object basevalue);
        private static object CoerceBlockMergingRegionIDCore(DependencyObject d, object baseValue);
        private static object CoerceRegionIDCore(DependencyObject d, object baseValue);
        private static string GetActualString(params object[] param);
        [Obsolete("Use the ElementMergingBehavior property instead")]
        public static bool? GetAllowMerging(DependencyObject obj);
        internal static bool? GetAllowMergingCore(DependencyObject obj);
        internal static bool GetBlockMergingCore(DependencyObject d);
        public static ElementMergingBehavior GetElementMergingBehavior(DependencyObject obj);
        internal static bool GetForceElementMerging(DependencyObject obj);
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static bool GetHideElements(DependencyObject obj);
        public static string GetName(DependencyObject obj);
        public static ToolBarMergeStyle GetToolBarMergeStyle(DependencyObject obj);
        private static void OnAllowMergingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnElementMergingBehaviorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private static void OnOverridesAllowMergingBehaviorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        [Obsolete("Use the ElementMergingBehavior property instead")]
        public static void SetAllowMerging(DependencyObject obj, bool? value);
        internal static void SetBlockMerging(DependencyObject d, bool value);
        public static void SetElementMergingBehavior(DependencyObject obj, ElementMergingBehavior value);
        private static void SetForceElementMerging(DependencyObject obj, bool value);
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static void SetHideElements(DependencyObject obj, bool value);
        public static void SetName(DependencyObject obj, string value);
        public static void SetToolBarMergeStyle(DependencyObject obj, ToolBarMergeStyle value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MergingProperties.<>c <>9;
            public static Func<object, bool> <>9__40_0;

            static <>c();
            internal void <.cctor>b__23_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__23_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__23_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <GetActualString>b__40_0(object x);
        }

        private class RegionID
        {
            private int hCode;
            private MergingProperties.RegionID parent;

            public RegionID(MergingProperties.RegionID parent);
            private bool Equals(MergingProperties.RegionID obj);
            public override bool Equals(object obj);
            public override int GetHashCode();
            private int GetHashCodeInternal();
        }
    }
}

