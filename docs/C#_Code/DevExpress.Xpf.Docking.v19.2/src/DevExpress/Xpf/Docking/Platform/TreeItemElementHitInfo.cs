﻿namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class TreeItemElementHitInfo : BaseCustomizationFormElementHitInfo
    {
        public TreeItemElementHitInfo(Point pt, TreeItemElement element, ILayoutController controller) : base(pt, element, controller)
        {
        }

        public override bool InMenuBounds =>
            base.InBounds && (!base.InContent || base.IsCustomization);
    }
}

