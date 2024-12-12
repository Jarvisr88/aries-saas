namespace DevExpress.Xpf.Grid
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Serialization;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public abstract class DetailDescriptorBase : DXFrameworkContentElement
    {
        private static readonly DataTemplate DefaultHeaderContentTemplate = XamlHelper.GetTemplate("<TextBlock Text=\"{Binding}\"/>");
        public static readonly DependencyProperty ShowHeaderProperty;
        public static readonly DependencyProperty HeaderContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty MarginProperty;
        private IDetailDescriptorOwner owner;
        private IEnumerable<DetailDescriptorContainer> dataControlDetailDescriptors;
        private DetailSynchronizationQueues synchronizationQueues;
        internal static readonly DataControlDetailDescriptor[] EmptyDetailDescriptors = new DataControlDetailDescriptor[0];

        static DetailDescriptorBase()
        {
            Type ownerType = typeof(DetailDescriptorBase);
            ShowHeaderProperty = DependencyPropertyManager.Register("ShowHeader", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((DetailDescriptorBase) d).InvalidateTree()));
            HeaderContentTemplateProperty = DependencyPropertyManager.Register("HeaderContentTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(DefaultHeaderContentTemplate));
            ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((DetailDescriptorBase) d).InvalidateTree()));
            Thickness defaultValue = new Thickness();
            MarginProperty = DependencyPropertyManager.Register("Margin", typeof(Thickness), ownerType, new FrameworkPropertyMetadata(defaultValue, (d, e) => ((DetailDescriptorBase) d).OnMarginChanged((Thickness) e.OldValue)));
        }

        public DetailDescriptorBase()
        {
            this.DetailViewIndents = new ObservableCollection<DetailIndent>();
        }

        private void AddOrChangeDetailViewIndent(int index, double left, double right, int level, bool isDetailMargin, bool isLast, ref int actualCount)
        {
            if (this.DetailViewIndents.Count == index)
            {
                this.DetailViewIndents.Add(new DetailIndent());
            }
            this.DetailViewIndents[index].Update(left, right, level, isDetailMargin, isLast);
            actualCount++;
        }

        protected virtual IEnumerable<DetailDescriptorContainer> CreateDataControlDetailDescriptors()
        {
            List<DetailDescriptorContainer> list1 = new List<DetailDescriptorContainer>();
            list1.Add(new DetailDescriptorContainer(null));
            return list1;
        }

        internal abstract DetailInfoWithContent CreateRowDetailInfo(RowDetailContainer container);
        internal abstract DataControlBase GetChildDataControl(DataControlBase dataControl, int rowHandle, object detailRow);
        protected internal virtual IEnumerable<DataControlDetailDescriptor> GetDetailDescriptors(DataTreeBuilder treeBuilder, int rowHandle) => 
            EmptyDetailDescriptors;

        protected internal virtual bool HasDataControlDetailDescriptor() => 
            false;

        internal void InvalidateTree()
        {
            this.Owner.InvalidateTree();
        }

        internal abstract void OnDetach();
        private void OnMarginChanged(Thickness oldValue)
        {
            if ((oldValue.Left != this.Margin.Left) || (oldValue.Right != this.Margin.Right))
            {
                this.Owner.InvalidateIndents();
            }
            if ((oldValue.Top != this.Margin.Top) || (oldValue.Bottom != this.Margin.Bottom))
            {
                this.InvalidateTree();
            }
        }

        protected virtual void OnMasterControlChanged()
        {
            this.UpdateMasterControl();
        }

        internal virtual void SynchronizeDetailTree()
        {
            this.SynchronizationQueues.SynchronizeUnsynchronizedNodes();
        }

        internal abstract void UpdateDetailDataControls(Action<DataControlBase> updateOpenDetailMethod, Action<DataControlBase> updateClosedDetailMethod = null);
        internal virtual void UpdateDetailViewIndents(ObservableCollection<DetailIndent> ownerIndents, Thickness margin)
        {
            int index = 0;
            int level = 1;
            if (ownerIndents != null)
            {
                foreach (DetailIndent indent in ownerIndents)
                {
                    this.AddOrChangeDetailViewIndent(index, indent.Width, indent.WidthAtRight, indent.Level, indent.IsDetailMargin, false, ref index);
                    if (!indent.IsDetailMargin)
                    {
                        level = indent.Level + 1;
                    }
                }
            }
            if ((margin.Left != 0.0) || (margin.Right != 0.0))
            {
                this.AddOrChangeDetailViewIndent(index, margin.Left, margin.Right, level, false, false, ref index);
            }
            if ((this.Margin.Left != 0.0) || (this.Margin.Right != 0.0))
            {
                this.AddOrChangeDetailViewIndent(index, this.Margin.Left, this.Margin.Right, level, true, true, ref index);
            }
            for (int i = this.DetailViewIndents.Count - 1; i > (index - 1); i--)
            {
                this.DetailViewIndents.RemoveAt(i);
            }
        }

        internal virtual void UpdateMasterControl()
        {
        }

        internal abstract void UpdateOriginationDataControls(Action<DataControlBase> updateMethod);

        internal IDetailDescriptorOwner Owner
        {
            get => 
                this.owner ?? NullDetailDescriptorOwner.Instance;
            set
            {
                if (!ReferenceEquals(this.owner, value))
                {
                    this.owner = value;
                    this.OnMasterControlChanged();
                }
            }
        }

        public virtual IEnumerable<DetailDescriptorContainer> DataControlDetailDescriptors
        {
            get
            {
                this.dataControlDetailDescriptors ??= this.CreateDataControlDetailDescriptors();
                return this.dataControlDetailDescriptors;
            }
        }

        public bool ShowHeader
        {
            get => 
                (bool) base.GetValue(ShowHeaderProperty);
            set => 
                base.SetValue(ShowHeaderProperty, value);
        }

        public ObservableCollection<DetailIndent> DetailViewIndents { get; protected set; }

        public DataTemplate HeaderContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(HeaderContentTemplateProperty);
            set => 
                base.SetValue(HeaderContentTemplateProperty, value);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public Thickness Margin
        {
            get => 
                (Thickness) base.GetValue(MarginProperty);
            set => 
                base.SetValue(MarginProperty, value);
        }

        internal DetailSynchronizationQueues SynchronizationQueues
        {
            get
            {
                this.synchronizationQueues ??= new DetailSynchronizationQueues();
                return this.synchronizationQueues;
            }
        }

        internal bool HasTopMargin =>
            this.Margin.Top > 0.0;

        internal bool HasBottomMargin =>
            this.Margin.Bottom > 0.0;

        [Description("Gets or sets the detail descriptor name."), XtraSerializableProperty, XtraResetProperty(ResetPropertyMode.None), GridStoreAlwaysProperty]
        public string Name
        {
            get => 
                base.Name;
            set => 
                base.Name = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailDescriptorBase.<>c <>9 = new DetailDescriptorBase.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailDescriptorBase) d).InvalidateTree();
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailDescriptorBase) d).InvalidateTree();
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DetailDescriptorBase) d).OnMarginChanged((Thickness) e.OldValue);
            }
        }
    }
}

