namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectionViewModel : FrameworkElement
    {
        public static readonly DependencyProperty SelectAllProperty;
        public static readonly DependencyProperty IsSelectedProperty;

        static SelectionViewModel()
        {
            Type ownerType = typeof(SelectionViewModel);
            SelectAllProperty = DependencyPropertyManager.Register("SelectAll", typeof(bool?), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((SelectionViewModel) d).SelectAllChanged((bool?) e.NewValue)));
            IsSelectedProperty = DependencyPropertyManager.Register("IsSelected", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((SelectionViewModel) d).IsSelectedChanged((bool) e.NewValue)));
        }

        public SelectionViewModel(Func<ISelectorEditInnerListBox> getListBox)
        {
            this.GetListBox = getListBox;
        }

        protected virtual void IsSelectedChanged(bool newValue)
        {
            ISelectorEditInnerListBox box = this.GetListBox();
            if (box != null)
            {
                box.SelectAllLocker.DoLockedActionIfNotLocked(() => this.SelectionChanged(new bool?(newValue)));
            }
        }

        protected virtual void SelectAllChanged(bool? newValue)
        {
            ISelectorEditInnerListBox box = this.GetListBox();
            if (box != null)
            {
                box.SelectAllLocker.DoLockedActionIfNotLocked(() => this.SelectionChanged(newValue));
            }
        }

        public virtual void SelectionChanged(bool? isSelectAll)
        {
            ISelectorEditInnerListBox box = this.GetListBox();
            if ((box != null) && (isSelectAll != null))
            {
                if (isSelectAll.Value)
                {
                    box.SelectAll();
                }
                else
                {
                    box.UnselectAll();
                }
            }
        }

        public virtual void SetSelectAllWithoutUpdates(bool? selectAll)
        {
            ISelectorEditInnerListBox box = this.GetListBox();
            if (box != null)
            {
                box.SelectAllLocker.DoLockedAction(delegate {
                    this.SelectAll = selectAll;
                    if (selectAll != null)
                    {
                        this.IsSelected = selectAll.Value;
                    }
                });
            }
        }

        public bool? SelectAll
        {
            get => 
                (bool?) base.GetValue(SelectAllProperty);
            set => 
                base.SetValue(SelectAllProperty, value);
        }

        public bool IsSelected
        {
            get => 
                (bool) base.GetValue(IsSelectedProperty);
            set => 
                base.SetValue(IsSelectedProperty, value);
        }

        private Func<ISelectorEditInnerListBox> GetListBox { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionViewModel.<>c <>9 = new SelectionViewModel.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectionViewModel) d).SelectAllChanged((bool?) e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SelectionViewModel) d).IsSelectedChanged((bool) e.NewValue);
            }
        }
    }
}

