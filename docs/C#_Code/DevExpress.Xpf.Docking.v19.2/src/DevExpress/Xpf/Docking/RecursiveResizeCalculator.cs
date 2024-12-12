namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    internal class RecursiveResizeCalculator : BaseResizeCalculator
    {
        protected List<ResizeInfo> Infos = new List<ResizeInfo>();
        private Dictionary<object, double> StarsHash = new Dictionary<object, double>();
        private Dictionary<object, double> PixelHash = new Dictionary<object, double>();

        private static double AbsMin(double value1, double value2) => 
            Math.Min(Math.Abs(value1), Math.Abs(value2));

        protected virtual void ApplyMeasure()
        {
            foreach (ResizeInfo info in this.Infos)
            {
                if (MathHelper.IsConstraintValid(info.Length))
                {
                    info.Item.ResizeLockHelper.Lock();
                    info.Item.SetValue((base.Orientation == Orientation.Horizontal) ? BaseLayoutItem.ItemWidthProperty : BaseLayoutItem.ItemHeightProperty, new GridLength(info.Length, info.GridUnitType));
                }
            }
            this.Infos.Clear();
        }

        protected virtual bool CanMeasureRecursively(LayoutGroup group) => 
            (group.Orientation == base.Orientation) && (!group.IgnoreOrientation && !group.IsControlItemsHost);

        private double GetChange(DefinitionBase def1, DefinitionBase def2, double change)
        {
            double num;
            double num2;
            this.GetDeltaConstraints(def1, def2, out num, out num2);
            return Math.Min(Math.Max(change, num), num2);
        }

        private void GetDeltaConstraints(DefinitionBase def1, DefinitionBase def2, out double minDelta, out double maxDelta)
        {
            double actualLength = base.GetActualLength(def1);
            double num2 = base.GetActualLength(def2);
            double num4 = DefinitionsHelper.UserMaxSizeValueCache(def1);
            double num6 = DefinitionsHelper.UserMaxSizeValueCache(def2);
            minDelta = -Math.Min((double) (actualLength - DefinitionsHelper.UserMinSizeValueCache(def1)), (double) (num6 - num2));
            maxDelta = Math.Min((double) (num4 - actualLength), (double) (num2 - DefinitionsHelper.UserMinSizeValueCache(def2)));
        }

        private double GetEffectiveLength(DefinitionBase definition)
        {
            GridLength length = definition.GetLength();
            double actualLength = definition.GetActualLength();
            return (length.IsAbsolute ? Math.Min(actualLength, length.Value) : actualLength);
        }

        private double GetHashedPixelValue(LayoutGroup parent)
        {
            double num = 0.0;
            if (this.PixelHash.Keys.Contains<object>(parent))
            {
                num = this.PixelHash[parent];
            }
            else
            {
                foreach (BaseLayoutItem item in parent.Items)
                {
                    DefinitionBase definition = DefinitionsHelper.GetDefinition(item);
                    if (DefinitionsHelper.IsStar(definition))
                    {
                        num += DefinitionsHelper.UserActualSizeValueCache(definition);
                    }
                }
                this.PixelHash.Add(parent, num);
            }
            return num;
        }

        private double GetHashedStarValue(LayoutGroup parent)
        {
            double num = 0.0;
            if (this.StarsHash.Keys.Contains<object>(parent))
            {
                num = this.StarsHash[parent];
            }
            else
            {
                foreach (BaseLayoutItem item in parent.Items)
                {
                    DefinitionBase definition = DefinitionsHelper.GetDefinition(item);
                    if (DefinitionsHelper.IsStar(definition))
                    {
                        num += DefinitionsHelper.UserSizeValueCache(definition).Value;
                    }
                }
                this.StarsHash.Add(parent, num);
            }
            return num;
        }

        private BaseLayoutItem GetItemToResize(LayoutGroup group, bool first)
        {
            BaseLayoutItem item2;
            IEnumerable<BaseLayoutItem> enumerable1 = first ? ((IEnumerable<BaseLayoutItem>) group.Items.ToArray<BaseLayoutItem>()) : group.Items.Reverse<BaseLayoutItem>();
            using (IEnumerator<BaseLayoutItem> enumerator = enumerable1.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BaseLayoutItem current = enumerator.Current;
                        if (!LayoutItemsHelper.IsResizable(current, group.Orientation == Orientation.Horizontal))
                        {
                            continue;
                        }
                        item2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item2;
        }

        public override void Init(DevExpress.Xpf.Docking.SplitBehavior splitBehavior)
        {
            base.Init(splitBehavior);
            this.StarsHash.Clear();
            this.PixelHash.Clear();
            this.Infos.Clear();
        }

        private void Measure(BaseLayoutItem def1, BaseLayoutItem def2, double change)
        {
            bool flag = true;
            if (def1 is LayoutGroup)
            {
                LayoutGroup group = (LayoutGroup) def1;
                if (this.CanMeasureRecursively(group))
                {
                    BaseLayoutItem itemToResize = this.GetItemToResize(group, false);
                    if (itemToResize != null)
                    {
                        flag = this.MeasureRecursive(itemToResize, def2, change, true);
                    }
                }
            }
            if (flag && (def2 is LayoutGroup))
            {
                LayoutGroup group = (LayoutGroup) def2;
                if (this.CanMeasureRecursively(group))
                {
                    BaseLayoutItem itemToResize = this.GetItemToResize(group, true);
                    if (itemToResize != null)
                    {
                        flag = this.MeasureRecursive(itemToResize, def1, -change, false);
                    }
                }
            }
            DefinitionBase definition = DefinitionsHelper.GetDefinition(def1);
            DefinitionBase base3 = DefinitionsHelper.GetDefinition(def2);
            change = this.GetChange(definition, base3, change);
            double effectiveLength = this.GetEffectiveLength(definition);
            double num2 = this.GetEffectiveLength(base3);
            double num3 = effectiveLength + change;
            double num4 = num2 - change;
            if ((effectiveLength == num3) || ((num2 == num4) || !flag))
            {
                this.Infos.Clear();
            }
            else
            {
                switch (base.SplitBehavior)
                {
                    case DevExpress.Xpf.Docking.SplitBehavior.Split:
                    {
                        if (!this.StarsHash.Keys.Contains<object>(definition))
                        {
                            this.StarsHash.Add(definition, DefinitionsHelper.UserSizeValueCache(definition).Value);
                        }
                        if (!this.StarsHash.Keys.Contains<object>(base3))
                        {
                            this.StarsHash.Add(base3, DefinitionsHelper.UserSizeValueCache(base3).Value);
                        }
                        double num5 = num3 / (num3 + num4);
                        double num6 = this.StarsHash[definition] + this.StarsHash[base3];
                        num3 = num5 * num6;
                        num4 = (1.0 - num5) * num6;
                        ResizeInfo item = new ResizeInfo();
                        item.Length = num3;
                        item.GridUnitType = GridUnitType.Star;
                        item.Item = def1;
                        this.Infos.Add(item);
                        ResizeInfo info2 = new ResizeInfo();
                        info2.Length = num4;
                        info2.GridUnitType = GridUnitType.Star;
                        info2.Item = def2;
                        this.Infos.Add(info2);
                        return;
                    }
                    case DevExpress.Xpf.Docking.SplitBehavior.Resize1:
                    {
                        LayoutGroup parent = def1.Parent;
                        double hashedStarValue = this.GetHashedStarValue(parent);
                        num4 = (num4 / this.GetHashedPixelValue(parent)) * hashedStarValue;
                        ResizeInfo item = new ResizeInfo();
                        item.Length = num3;
                        item.GridUnitType = GridUnitType.Pixel;
                        item.Item = def1;
                        this.Infos.Add(item);
                        ResizeInfo info4 = new ResizeInfo();
                        info4.Length = num4;
                        info4.GridUnitType = GridUnitType.Star;
                        info4.Item = def2;
                        this.Infos.Add(info4);
                        return;
                    }
                    case DevExpress.Xpf.Docking.SplitBehavior.Resize2:
                    {
                        LayoutGroup parent = def1.Parent;
                        double hashedStarValue = this.GetHashedStarValue(parent);
                        num3 = (num3 / this.GetHashedPixelValue(parent)) * hashedStarValue;
                        ResizeInfo item = new ResizeInfo();
                        item.Length = num4;
                        item.GridUnitType = GridUnitType.Pixel;
                        item.Item = def2;
                        this.Infos.Add(item);
                        ResizeInfo info6 = new ResizeInfo();
                        info6.Length = num3;
                        info6.GridUnitType = GridUnitType.Star;
                        info6.Item = def1;
                        this.Infos.Add(info6);
                        return;
                    }
                    case DevExpress.Xpf.Docking.SplitBehavior.PixelSplit:
                    {
                        ResizeInfo item = new ResizeInfo();
                        item.Length = num3;
                        item.GridUnitType = GridUnitType.Pixel;
                        item.Item = def1;
                        this.Infos.Add(item);
                        ResizeInfo info8 = new ResizeInfo();
                        info8.Length = num4;
                        info8.GridUnitType = GridUnitType.Pixel;
                        info8.Item = def2;
                        this.Infos.Add(info8);
                        return;
                    }
                }
            }
        }

        private double MeasureChange(BaseLayoutItem def1, BaseLayoutItem def2, double change)
        {
            double num5;
            double num = change;
            if (def1 is LayoutGroup)
            {
                LayoutGroup group = (LayoutGroup) def1;
                if (this.CanMeasureRecursively(group))
                {
                    BaseLayoutItem itemToResize = this.GetItemToResize(group, false);
                    if (itemToResize != null)
                    {
                        num = AbsMin(num, this.MeasureChangeRecursive(itemToResize, def2, change, true));
                    }
                }
            }
            if (def2 is LayoutGroup)
            {
                LayoutGroup group = (LayoutGroup) def2;
                if (this.CanMeasureRecursively(group))
                {
                    BaseLayoutItem itemToResize = this.GetItemToResize(group, true);
                    if (itemToResize != null)
                    {
                        num = AbsMin(num, this.MeasureChangeRecursive(itemToResize, def1, -change, false));
                    }
                }
            }
            DefinitionBase definition = DefinitionsHelper.GetDefinition(def1);
            DefinitionBase base3 = DefinitionsHelper.GetDefinition(def2);
            double effectiveLength = this.GetEffectiveLength(definition);
            double num3 = this.GetEffectiveLength(base3);
            double num4 = effectiveLength + num3;
            double num6 = Math.Min((double) (this.GetMin(def1) + this.GetMin(def2)), (double) (num4 * 0.5));
            if (change < 0.0)
            {
                num6 = Math.Max(num6, DefinitionsHelper.UserMinSizeValueCache(definition));
                num5 = Math.Max(num4 - num6, DefinitionsHelper.UserMinSizeValueCache(base3));
            }
            else
            {
                num6 = Math.Max(num6, DefinitionsHelper.UserMinSizeValueCache(base3));
                num5 = Math.Max(num4 - num6, DefinitionsHelper.UserMinSizeValueCache(definition));
            }
            num = AbsMin(MathHelper.MeasureDimension(num6, num5, effectiveLength + change) - effectiveLength, num);
            num = AbsMin(MathHelper.MeasureDimension(num6, num5, num3 - change) - num3, num);
            return ((change < 0.0) ? -num : num);
        }

        private double MeasureChangeRecursive(BaseLayoutItem def1, BaseLayoutItem def2, double change, bool first = true)
        {
            DefinitionBase definition = DefinitionsHelper.GetDefinition(def1);
            DefinitionBase base3 = DefinitionsHelper.GetDefinition(def2);
            double actualLength = base.GetActualLength(definition);
            double num3 = actualLength + base.GetActualLength(base3);
            double min = Math.Max(Math.Min((double) (this.GetMin(def1) + this.GetMin(def2)), (double) (num3 * 0.5)), DefinitionsHelper.UserMinSizeValueCache(definition));
            double num6 = MathHelper.MeasureDimension(min, Math.Min(num3 - min, DefinitionsHelper.UserMaxSizeValueCache(definition)), actualLength + change);
            double num7 = change;
            if (def1 is LayoutGroup)
            {
                LayoutGroup group = (LayoutGroup) def1;
                if (this.CanMeasureRecursively(group))
                {
                    BaseLayoutItem itemToResize = this.GetItemToResize(group, first);
                    if (itemToResize != null)
                    {
                        num7 = AbsMin(num7, this.MeasureChangeRecursive(itemToResize, def2, change, true));
                    }
                }
            }
            return AbsMin(num7, actualLength - num6);
        }

        private bool MeasureRecursive(BaseLayoutItem def1, BaseLayoutItem def2, double change, bool first = true)
        {
            DefinitionBase definition = DefinitionsHelper.GetDefinition(def1);
            DefinitionBase base3 = DefinitionsHelper.GetDefinition(def2);
            change = this.GetChange(definition, base3, change);
            double actualLength = base.GetActualLength(definition);
            double num3 = actualLength + base.GetActualLength(base3);
            double num4 = Math.Min((double) (this.GetMin(def1) + this.GetMin(def2)), (double) (num3 * 0.5));
            double num5 = actualLength + change;
            if ((actualLength + change) < num4)
            {
                return false;
            }
            if ((def1 is LayoutGroup) && ((((LayoutGroup) def1).Orientation == base.Orientation) && !((LayoutGroup) def1).IgnoreOrientation))
            {
                LayoutGroup group = (LayoutGroup) def1;
                BaseLayoutItem itemToResize = this.GetItemToResize(group, first);
                if (itemToResize != null)
                {
                    this.MeasureRecursive(itemToResize, def2, change, true);
                }
            }
            if (DefinitionsHelper.IsStar(definition))
            {
                LayoutGroup parent = def1.Parent;
                double hashedStarValue = this.GetHashedStarValue(parent);
                num5 = (num5 / this.GetHashedPixelValue(parent)) * hashedStarValue;
                ResizeInfo item = new ResizeInfo();
                item.Length = num5;
                item.GridUnitType = GridUnitType.Star;
                item.Item = def1;
                this.Infos.Add(item);
            }
            if (DefinitionsHelper.IsAbsolute(definition))
            {
                ResizeInfo item = new ResizeInfo();
                item.Length = num5;
                item.GridUnitType = GridUnitType.Pixel;
                item.Item = def1;
                this.Infos.Add(item);
            }
            return true;
        }

        public override void Resize(BaseLayoutItem def1, BaseLayoutItem def2, double change)
        {
            double num = this.MeasureChange(def1, def2, change);
            this.Measure(def1, def2, num);
            this.ApplyMeasure();
        }

        public class ResizeInfo
        {
            public double Length { get; set; }

            public System.Windows.GridUnitType GridUnitType { get; set; }

            public BaseLayoutItem Item { get; set; }
        }
    }
}

