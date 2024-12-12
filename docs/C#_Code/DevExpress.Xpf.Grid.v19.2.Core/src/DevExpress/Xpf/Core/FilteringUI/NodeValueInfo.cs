namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.FilteringUI.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NodeValueInfo : INotifyPropertyChanged
    {
        public DevExpress.Xpf.Core.FilteringUI.Native.DisplayMode DisplayMode { get; }
        public object Value { get; }
        public Func<string> GetDisplayText { get; }
        public Lazy<string> DisplayTextLazy { get; }
        public BaseEditSettings EditSettings =>
            this.EditSettingsLazy.Value;
        public Lazy<BaseEditSettings> EditSettingsLazy { get; }
        internal NodeValueInfo(object value, Func<string> getDisplayText, DevExpress.Xpf.Core.FilteringUI.Native.DisplayMode displayMode, Lazy<BaseEditSettings> editSettingsLazy)
        {
            Func<string> func1 = getDisplayText;
            if (getDisplayText == null)
            {
                Func<string> local1 = getDisplayText;
                func1 = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Func<string> local2 = <>c.<>9__17_0;
                    func1 = <>c.<>9__17_0 = (Func<string>) (() => null);
                }
            }
            getDisplayText = func1;
            this.<DisplayTextLazy>k__BackingField = new Lazy<string>(getDisplayText);
            this.<Value>k__BackingField = value;
            this.<DisplayMode>k__BackingField = displayMode;
            this.<GetDisplayText>k__BackingField = getDisplayText;
            this.<EditSettingsLazy>k__BackingField = editSettingsLazy;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }
        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NodeValueInfo.<>c <>9 = new NodeValueInfo.<>c();
            public static Func<string> <>9__17_0;

            internal string <.ctor>b__17_0() => 
                null;
        }
    }
}

