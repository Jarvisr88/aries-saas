namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ControlEx : Control, IVisualTransformOwner
    {
        Transform IVisualTransformOwner.VisualTransform { get; set; }
    }
}

