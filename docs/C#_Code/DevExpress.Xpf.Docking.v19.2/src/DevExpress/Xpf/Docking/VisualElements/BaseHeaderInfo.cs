namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BaseHeaderInfo : ITabHeaderInfo
    {
        public BaseHeaderInfo(UIElement tabHeader, FrameworkElement caption, BaseControlBoxControl controlBox, bool selected) : this(tabHeader, caption, true, controlBox, selected, TabHeaderPinLocation.Default, false)
        {
        }

        public BaseHeaderInfo(UIElement tabHeader, FrameworkElement caption, bool isCaptionHorz, BaseControlBoxControl controlBox, bool selected, TabHeaderPinLocation pinLocation, bool isPinned)
        {
            this.TabHeader = tabHeader;
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(tabHeader);
            LayoutGroup group = layoutItem.Parent ?? (LogicalTreeHelper.GetParent(layoutItem) as LayoutGroup);
            if (group != null)
            {
                this.Index = group.TabIndexFromItem(layoutItem);
            }
            this.DesiredSize = tabHeader.DesiredSize;
            CaptionControl control = caption as CaptionControl;
            if (caption != null)
            {
                if (control == null)
                {
                    this.CaptionText = caption.DesiredSize;
                }
                else
                {
                    if (control.PartText != null)
                    {
                        this.CaptionText = control.PartText.DesiredSize;
                    }
                    if (control.PartImage != null)
                    {
                        this.CaptionImage = control.PartImage.DesiredSize;
                    }
                    if (control.PartSpace != null)
                    {
                        this.CaptionImageToCaptionDistance = control.PartSpace.Width.Value;
                    }
                }
            }
            if (controlBox != null)
            {
                this.ControlBox = controlBox.DesiredSize;
                this.CaptionToControlBoxDistance = 0.0;
            }
            this.IsSelected = selected;
            if (!isCaptionHorz)
            {
                this.CaptionText = new Size(this.CaptionText.Height, this.CaptionText.Width);
                this.CaptionImage = new Size(this.CaptionImage.Height, this.CaptionImage.Width);
            }
            if ((caption != null) && (caption.IsVisible && (control == null)))
            {
                Size[] minSizes = new Size[] { this.DesiredSize, this.CaptionText };
                this.DesiredSize = MathHelper.MeasureMinSize(minSizes);
            }
            this.PinLocation = pinLocation;
            this.IsPinned = isPinned;
        }

        public int Index { get; private set; }

        public object TabHeader { get; private set; }

        public System.Windows.Rect Rect { get; set; }

        public bool IsVisible { get; set; }

        public bool IsSelected { get; set; }

        public bool ShowCaption { get; set; }

        public bool ShowCaptionImage { get; set; }

        public int ZIndex { get; set; }

        public Size DesiredSize { get; private set; }

        public Size CaptionImage { get; private set; }

        public Size CaptionText { get; private set; }

        public Size ControlBox { get; private set; }

        public double CaptionImageToCaptionDistance { get; private set; }

        public double CaptionToControlBoxDistance { get; private set; }

        public TabHeaderPinLocation PinLocation { get; private set; }

        public bool IsPinned { get; private set; }

        public int ScrollIndex { get; set; }

        public IMultiLineLayoutResult MultiLineResult { get; set; }
    }
}

