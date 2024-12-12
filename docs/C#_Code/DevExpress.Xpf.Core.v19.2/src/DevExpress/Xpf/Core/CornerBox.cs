namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Native;
    using System;

    public class CornerBox : ControlBase
    {
        static CornerBox()
        {
            DependencyPropertyRegistrator<CornerBox>.New().OverrideDefaultStyleKey();
        }
    }
}

