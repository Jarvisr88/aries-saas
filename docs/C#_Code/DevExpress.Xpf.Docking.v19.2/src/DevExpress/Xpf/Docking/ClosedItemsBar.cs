namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class ClosedItemsBar : Bar
    {
        private ClosedItemsPanel panel;

        public ClosedItemsBar(ClosedItemsPanel panel)
        {
            this.Container = panel.Container;
            this.UpdatePanel(panel);
            base.Caption = this.Panel.Category.Name;
            base.UseWholeRow = DefaultBoolean.True;
            base.AllowQuickCustomization = DefaultBoolean.False;
            base.AllowCustomizationMenu = false;
            base.ShowDragWidget = false;
            this.Container.LockClosedPanelsVisibility();
            try
            {
                base.Visible = false;
            }
            finally
            {
                this.Container.UnlockClosedPanelsVisibility();
            }
            this.CheckCategory();
            MergingProperties.SetElementMergingBehavior(this, ElementMergingBehavior.None);
        }

        public void AddItems(IList items)
        {
            this.EnsureBarInfo();
            foreach (BaseLayoutItem item in items)
            {
                if (this.GetLink(item) == null)
                {
                    this.BarInfo.CreateBarButtonItem(item);
                }
            }
        }

        protected void CheckCategory()
        {
            BarManager barManager = BarManager.GetBarManager(this);
            if ((barManager != null) && !barManager.Categories.Contains(this.Panel.Category))
            {
                barManager.Categories.Add(this.Panel.Category);
            }
        }

        public void ClearItems()
        {
            this.EnsureBarInfo();
            IBarItem[] array = new IBarItem[base.Items.Count];
            base.Items.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                base.Items.Remove(array[i]);
            }
        }

        public BarButtonItem CreateBarButtonItem(object content)
        {
            BaseLayoutItem source = content as BaseLayoutItem;
            BarButtonItem item1 = new BarButtonItem();
            item1.CategoryName = this.Panel.Category.Name;
            item1.Glyph = source.CaptionImage;
            BarButtonItem target = item1;
            BindingHelper.SetBinding(target, BarItem.ContentProperty, source, BaseLayoutItem.ActualCustomizationCaptionProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, BarItem.ContentTemplateProperty, source, BaseLayoutItem.ActualCustomizationCaptionTemplateProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, BarItem.ContentTemplateSelectorProperty, source, BaseLayoutItem.ActualCustomizationCaptionTemplateSelectorProperty, BindingMode.OneWay);
            target.Tag = source;
            base.Items.Add(target);
            return target;
        }

        protected virtual ClosedItemsBarInfo CreateBarInfo(DockLayoutManager container) => 
            new ClosedItemsBarInfo(this, container);

        protected void EnsureBarInfo()
        {
            this.BarInfo ??= this.CreateBarInfo(this.Container);
        }

        private BarItem GetLink(BaseLayoutItem item)
        {
            BarItem item3;
            using (IEnumerator<IBarItem> enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        BarItem current = (BarItem) enumerator.Current;
                        if (current.Tag != item)
                        {
                            continue;
                        }
                        item3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return item3;
        }

        internal void InvalidateContainerName()
        {
            base.DockInfo.ContainerName = null;
            base.DockInfo.ContainerName = ((this.Panel == null) || !base.Visible) ? null : this.Panel.Name;
        }

        protected virtual void OnPanelChanged(ClosedItemsPanel oldValue)
        {
            base.DockInfo.Container = this.Panel;
        }

        protected override void OnVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            base.DockInfo.ContainerName = ((bool) e.NewValue) ? this.Panel.Name : null;
            base.OnVisibleChanged(e);
            if (!this.Container.IsClosedPanelsVisibilityLocked && (this.Container.ClosedPanelsBarVisibility == ClosedPanelsBarVisibility.Auto))
            {
                this.Container.ClosedPanelsBarVisibility = ClosedPanelsBarVisibility.Manual;
            }
        }

        public void RemoveItems(IList items)
        {
            this.EnsureBarInfo();
            foreach (BaseLayoutItem item in items)
            {
                BarItem link = this.GetLink(item);
                if (link != null)
                {
                    base.Items.Remove(link);
                }
            }
        }

        public void ResetItems(IList items)
        {
            this.ClearItems();
            this.AddItems(items);
        }

        public void UpdateItems(IList items)
        {
            this.EnsureBarInfo();
            foreach (BaseLayoutItem item in items)
            {
                BarItem link = this.GetLink(item);
                if (link != null)
                {
                    base.Items.Remove(link);
                }
                this.BarInfo.CreateBarButtonItem(item);
            }
        }

        protected internal void UpdatePanel(ClosedItemsPanel panel)
        {
            if (panel == null)
            {
                base.DockInfo.Reset();
                this.ClearItems();
            }
            this.Panel = panel;
        }

        public DockLayoutManager Container { get; private set; }

        public ClosedItemsPanel Panel
        {
            get => 
                this.panel;
            private set
            {
                if (!ReferenceEquals(value, this.panel))
                {
                    ClosedItemsPanel oldValue = this.panel;
                    this.panel = value;
                    this.OnPanelChanged(oldValue);
                }
            }
        }

        protected ClosedItemsBarInfo BarInfo { get; private set; }
    }
}

