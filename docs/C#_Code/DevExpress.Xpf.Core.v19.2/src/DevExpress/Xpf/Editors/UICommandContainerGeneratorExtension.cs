namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class UICommandContainerGeneratorExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            MessageBoxResult? defaultButton = null;
            defaultButton = null;
            Func<UICommand, DevExpress.Xpf.Editors.UICommandContainer> selector = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<UICommand, DevExpress.Xpf.Editors.UICommandContainer> local1 = <>c.<>9__4_0;
                selector = <>c.<>9__4_0 = x => new DevExpress.Xpf.Editors.UICommandContainer(x);
            }
            return new DevExpress.Xpf.Editors.UICommandContainerCollection(UICommand.GenerateFromMessageBoxButton(this.Buttons, new DXDialogWindowMessageBoxButtonLocalizer(), defaultButton, defaultButton).Select<UICommand, DevExpress.Xpf.Editors.UICommandContainer>(selector));
        }

        public MessageBoxButton Buttons { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UICommandContainerGeneratorExtension.<>c <>9 = new UICommandContainerGeneratorExtension.<>c();
            public static Func<UICommand, DevExpress.Xpf.Editors.UICommandContainer> <>9__4_0;

            internal DevExpress.Xpf.Editors.UICommandContainer <ProvideValue>b__4_0(UICommand x) => 
                new DevExpress.Xpf.Editors.UICommandContainer(x);
        }
    }
}

