namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ExtendedColumnChooserColumn : BindableBase, IDisposable
    {
        private UnsubscribeAction unsubscribeAction;
        private IBandsOwner RootBand;

        public ExtendedColumnChooserColumn(IBandsOwner rootBand, BaseColumn source, bool isVisibleInHeaders)
        {
            this.RootBand = rootBand;
            this.Source = source;
            this.InitializeValues(isVisibleInHeaders);
            this.SubscribePropertyChanges();
        }

        private bool CanChangeBandVisibility(bool canStartDragSingleColumn, bool isVisibleInHeaders) => 
            canStartDragSingleColumn || !isVisibleInHeaders;

        private bool CanChangeColumnVisibility(bool canStartDragSingleColumn, bool isVisibleInHeaders) => 
            (canStartDragSingleColumn & isVisibleInHeaders) && ((this.ParentBand != null) || !this.Source.View.IsLastVisibleColumn(this.Source));

        public void Dispose()
        {
            if (this.unsubscribeAction == null)
            {
                UnsubscribeAction unsubscribeAction = this.unsubscribeAction;
            }
            else
            {
                this.unsubscribeAction();
            }
            this.unsubscribeAction = null;
        }

        private void DoIfColumnsNotLocked(Action action)
        {
            bool flag1;
            DataViewBase view = this.Source.View;
            if (view == null)
            {
                DataViewBase local1 = view;
                flag1 = false;
            }
            else
            {
                bool? nullable1;
                DataControlBase dataControl = view.DataControl;
                if (dataControl != null)
                {
                    nullable1 = new bool?(dataControl.ColumnsCore.IsLockUpdate);
                }
                else
                {
                    DataControlBase local2 = dataControl;
                    nullable1 = null;
                }
                bool? nullable = nullable1;
                bool flag = true;
                flag1 = (nullable.GetValueOrDefault() == flag) ? (nullable != null) : false;
            }
            if (!flag1)
            {
                action();
            }
        }

        internal static ExtendedColumnChooserColumn Factory(IBandsOwner rootBand, BaseColumn source, bool isVisibleInHeaders, ref UnsubscribeAction unsubscribeAction)
        {
            ExtendedColumnChooserColumn column = new ExtendedColumnChooserColumn(rootBand, source, isVisibleInHeaders);
            unsubscribeAction += column.unsubscribeAction;
            return column;
        }

        private void InitializeValues(bool isVisibleInHeaders)
        {
            this.UpdateIsVisible();
            this.UpdateVisibleIndex();
            this.UpdateHeader();
            this.UpdateParentBand();
            this.UpdateFixed();
            this.UpdateAllowHide(isVisibleInHeaders);
        }

        private void OnIsVisibleChanged()
        {
            if (this.Source != null)
            {
                this.Source.Visible = this.IsVisible;
            }
        }

        private void SubscribePropertyChanges()
        {
            UnsubscribeAction action1;
            UnsubscribeAction action2;
            DependencyProperty[] properties = new DependencyProperty[] { BaseColumn.VisibleProperty };
            DependencyProperty[] propertyArray2 = new DependencyProperty[] { BaseColumn.VisibleIndexProperty };
            DependencyProperty[] propertyArray3 = new DependencyProperty[] { BaseColumn.ParentBandProperty };
            ColumnBase source = this.Source as ColumnBase;
            if (source == null)
            {
                ColumnBase local1 = source;
                action1 = null;
            }
            else
            {
                DependencyProperty[] propertyArray4 = new DependencyProperty[] { ColumnBase.ActualColumnChooserHeaderCaptionProperty };
                action1 = source.AddPropertyChanged(new EventHandler(this.UpdateHeader), propertyArray4);
            }
            BandBase base2 = this.Source as BandBase;
            if (base2 == null)
            {
                BandBase local2 = base2;
                action2 = null;
            }
            else
            {
                DependencyProperty[] propertyArray5 = new DependencyProperty[] { BaseColumn.HeaderCaptionProperty };
                action2 = base2.AddPropertyChanged(new EventHandler(this.UpdateHeader), propertyArray5);
            }
            this.unsubscribeAction += (((this.Source.AddPropertyChanged(new EventHandler(this.UpdateIsVisible), properties) + this.Source.AddPropertyChanged(new EventHandler(this.UpdateVisibleIndex), propertyArray2)) + this.Source.AddPropertyChanged(new EventHandler(this.UpdateParentBand), propertyArray3)) + action1) + action2;
            this.unsubscribeAction += delegate {
                this.Source = null;
                this.RootBand = null;
            };
        }

        public void UpdateAllowHide(bool isVisibleInHeaders)
        {
            bool canStartDragSingleColumn = (this.Source.View != null) && this.Source.CanStartDragSingleColumn;
            this.AllowChangeVisibility = (this.Source is BandBase) ? this.CanChangeBandVisibility(canStartDragSingleColumn, isVisibleInHeaders) : this.CanChangeColumnVisibility(canStartDragSingleColumn, isVisibleInHeaders);
        }

        public void UpdateFixed()
        {
            if (this.Source.View != null)
            {
                this.Fixed = this.Source.View.ViewBehavior.GetActualColumnFixed(this.Source);
            }
        }

        private void UpdateHeader()
        {
            ColumnBase source = this.Source as ColumnBase;
            this.Header = ((source != null) ? source.ActualColumnChooserHeaderCaption : this.Source.HeaderCaption)?.ToString();
        }

        private void UpdateHeader(object sender, EventArgs e)
        {
            this.DoIfColumnsNotLocked(new Action(this.UpdateHeader));
        }

        private void UpdateIsVisible()
        {
            this.IsVisible = this.Source.Visible;
        }

        private void UpdateIsVisible(object sender, EventArgs e)
        {
            this.DoIfColumnsNotLocked(new Action(this.UpdateIsVisible));
        }

        private void UpdateParentBand()
        {
            IBandsOwner owner;
            BandBase parentBand = this.Source.ParentBand;
            if (owner == null)
            {
                BandBase local1 = this.Source.ParentBand;
                parentBand = (BandBase) this.RootBand;
            }
            this.ParentBand = parentBand;
        }

        private void UpdateParentBand(object sender, EventArgs e)
        {
            this.DoIfColumnsNotLocked(new Action(this.UpdateParentBand));
        }

        private void UpdateVisibleIndex()
        {
            this.VisibleIndex = this.Source.VisibleIndex;
        }

        private void UpdateVisibleIndex(object sender, EventArgs e)
        {
            this.DoIfColumnsNotLocked(() => this.UpdateVisibleIndex());
        }

        public BaseColumn Source { get; private set; }

        public int VisibleIndex
        {
            get => 
                base.GetValue<int>("VisibleIndex");
            private set => 
                base.SetValue<int>(value, "VisibleIndex");
        }

        public bool IsVisible
        {
            get => 
                base.GetValue<bool>("IsVisible");
            set => 
                base.SetValue<bool>(value, new Action(this.OnIsVisibleChanged), "IsVisible");
        }

        public string Header
        {
            get => 
                base.GetValue<string>("Header");
            private set => 
                base.SetValue<string>(value, "Header");
        }

        public IBandsOwner ParentBand
        {
            get => 
                base.GetValue<IBandsOwner>("ParentBand");
            private set => 
                base.SetValue<IBandsOwner>(value, "ParentBand");
        }

        public FixedStyle Fixed
        {
            get => 
                base.GetValue<FixedStyle>("Fixed");
            private set => 
                base.SetValue<FixedStyle>(value, "Fixed");
        }

        public bool AllowChangeVisibility
        {
            get => 
                base.GetValue<bool>("AllowChangeVisibility");
            private set => 
                base.SetValue<bool>(value, "AllowChangeVisibility");
        }

        public bool IsGrouped
        {
            get => 
                base.GetValue<bool>("IsGrouped");
            internal set => 
                base.SetValue<bool>(value, "IsGrouped");
        }
    }
}

