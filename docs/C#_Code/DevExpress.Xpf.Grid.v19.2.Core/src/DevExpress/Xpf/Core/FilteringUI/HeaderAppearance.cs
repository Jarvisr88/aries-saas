namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    internal sealed class HeaderAppearance
    {
        internal HeaderAppearance(object content, DataTemplateSelector selector)
        {
            this.<Content>k__BackingField = content;
            this.<Selector>k__BackingField = selector;
        }

        public object Content { get; }

        public DataTemplateSelector Selector { get; }
    }
}

