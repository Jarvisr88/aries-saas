namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class DockHintHitInfo
    {
        public DockHintHitInfo(DockingHintAdornerBase adorner, HitTestResult hitResult)
        {
            this.Adorner = adorner;
            this.Type = DockVisualizerElement.None;
            if (hitResult != null)
            {
                this.Element = LayoutHelper.FindParentObject<DockHintElement>(hitResult.VisualHit);
                if ((this.Element != null) && (this.Element.Visibility == Visibility.Visible))
                {
                    this.Type = this.Element.Type;
                    this.IsCenter = this.Type == DockVisualizerElement.Center;
                    this.DockType = this.CalcDockType(hitResult.VisualHit);
                    this.Dock = this.DockType.ToDock();
                    this.HitButton = LayoutHelper.FindParentObject<DockHintButton>(hitResult.VisualHit);
                    if (!this.InButton)
                    {
                        this.DockType = DevExpress.Xpf.Layout.Core.DockType.None;
                    }
                    else
                    {
                        this.IsHideButton = (this.HitButton.Name == "PART_Hide") && this.HitButton.IsEnabled;
                        this.IsTabButton = this.HitButton.Name.Contains("PART_Tab") && this.HitButton.IsEnabled;
                        if (!this.HitButton.IsAvailable)
                        {
                            this.DockType = DevExpress.Xpf.Layout.Core.DockType.None;
                        }
                    }
                }
            }
        }

        private DevExpress.Xpf.Layout.Core.DockType CalcDockType(DependencyObject dObj)
        {
            DevExpress.Xpf.Layout.Core.DockType none = DevExpress.Xpf.Layout.Core.DockType.None;
            while (true)
            {
                if (dObj != null)
                {
                    DevExpress.Xpf.Layout.Core.DockType dockType = DockHintElement.GetDockType(dObj);
                    if (dockType != DevExpress.Xpf.Layout.Core.DockType.None)
                    {
                        return dockType;
                    }
                    if (!(dObj is DockHintElement))
                    {
                        dObj = VisualTreeHelper.GetParent(dObj);
                        continue;
                    }
                }
                return none;
            }
        }

        protected DockHintElement Element { get; private set; }

        protected internal DockHintButton HitButton { get; private set; }

        public DockingHintAdornerBase Adorner { get; private set; }

        public DockVisualizerElement Type { get; private set; }

        public DevExpress.Xpf.Layout.Core.DockType DockType { get; private set; }

        public System.Windows.Controls.Dock Dock { get; private set; }

        public bool InHint =>
            (this.Element is SideDockHintElement) || (this.Element is CenterDockHintElement);

        public bool InButton =>
            this.HitButton != null;

        public bool IsHideButton { get; private set; }

        public bool IsCenter { get; private set; }

        public bool IsTabButton { get; private set; }
    }
}

