namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows.Controls;

    public interface IResizeCalculator
    {
        void Init(DevExpress.Xpf.Docking.SplitBehavior splitBehavior);
        void Resize(BaseLayoutItem item1, BaseLayoutItem item2, double change);

        System.Windows.Controls.Orientation Orientation { get; set; }
    }
}

