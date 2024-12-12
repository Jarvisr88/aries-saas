namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public interface IBadgeHost : IInputElement
    {
        bool CalculateBounds(Size badgeSize, HorizontalAlignment horizontalAlignment, HorizontalAlignment horizontalAnchor, VerticalAlignment verticalAlignment, VerticalAlignment verticalAnchor, ref Rect targetBounds, ref Rect badgeBounds);
    }
}

