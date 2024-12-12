namespace DevExpress.Xpf.Editors.DataPager
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DataPagerButton : Button
    {
        public static readonly DependencyProperty ButtonTypeProperty;
        public static readonly DependencyProperty DisplayModeProperty;
        public static readonly DependencyProperty IsCurrentPageProperty;
        public static readonly DependencyProperty ShowEllipsisProperty;
        public static readonly DependencyProperty PageNumberProperty;

        static DataPagerButton()
        {
            Type ownerType = typeof(DataPagerButton);
            ButtonTypeProperty = DependencyPropertyManager.Register("ButtonType", typeof(DataPagerButtonType), ownerType, new PropertyMetadata(null));
            DisplayModeProperty = DependencyPropertyManager.Register("DisplayMode", typeof(DataPagerDisplayMode), ownerType, new PropertyMetadata(DataPagerDisplayMode.FirstLastPreviousNextNumeric, (d, e) => ((DataPagerButton) d).OnDisplayModeChanged((DataPagerDisplayMode) e.OldValue)));
            IsCurrentPageProperty = DependencyPropertyManager.Register("IsCurrentPage", typeof(bool), ownerType, new PropertyMetadata((d, e) => ((DataPagerButton) d).OnIsCurrentPageChanged((bool) e.OldValue)));
            ShowEllipsisProperty = DependencyPropertyManager.Register("ShowEllipsis", typeof(bool), ownerType, new PropertyMetadata(null));
            PageNumberProperty = DependencyPropertyManager.Register("PageNumber", typeof(int), ownerType, new PropertyMetadata(0));
        }

        public DataPagerButton()
        {
            this.SetDefaultStyleKey(typeof(DataPagerButton));
        }

        public override void OnApplyTemplate()
        {
            this.SetStates(false);
        }

        protected virtual void OnDisplayModeChanged(DataPagerDisplayMode oldValue)
        {
            this.SetStates(true);
        }

        protected virtual void OnIsCurrentPageChanged(bool oldValue)
        {
            this.UpdateNumericButtonState(false);
        }

        private void SetPairState(DataPagerButtonType firstType, string stateName, DataPagerButtonType secondType, string secondStateName, bool useTransitions)
        {
            if (this.ButtonType == firstType)
            {
                VisualStateManager.GoToState(this, stateName, useTransitions);
            }
            else if (this.ButtonType == secondType)
            {
                VisualStateManager.GoToState(this, secondStateName, useTransitions);
            }
        }

        private void SetStates(bool useTransitions)
        {
            switch (this.DisplayMode)
            {
                case DataPagerDisplayMode.FirstLast:
                case DataPagerDisplayMode.FirstLastNumeric:
                    this.SetPairState(DataPagerButtonType.PageFirst, "FirstLeft", DataPagerButtonType.PageLast, "FirstRight", useTransitions);
                    this.UpdateNumericButtonState(useTransitions);
                    return;

                case DataPagerDisplayMode.FirstLastPreviousNext:
                case DataPagerDisplayMode.FirstLastPreviousNextNumeric:
                    this.SetPairState(DataPagerButtonType.PageFirst, "FirstLeft", DataPagerButtonType.PageLast, "FirstRight", useTransitions);
                    this.SetPairState(DataPagerButtonType.PagePrevious, "SecondLeft", DataPagerButtonType.PageNext, "SecondRight", useTransitions);
                    this.UpdateNumericButtonState(useTransitions);
                    return;

                case DataPagerDisplayMode.Numeric:
                    break;

                case DataPagerDisplayMode.PreviousNextNumeric:
                case DataPagerDisplayMode.PreviousNext:
                    this.SetPairState(DataPagerButtonType.PagePrevious, "FirstLeft", DataPagerButtonType.PageNext, "FirstRight", useTransitions);
                    this.UpdateNumericButtonState(useTransitions);
                    break;

                default:
                    return;
            }
        }

        private void UpdateNumericButtonState(bool useTransitions)
        {
            if (this.ButtonType == DataPagerButtonType.PageNumeric)
            {
                VisualStateManager.GoToState(this, this.IsCurrentPage ? "Selected" : "NotSelected", useTransitions);
            }
        }

        public DataPagerButtonType ButtonType
        {
            get => 
                (DataPagerButtonType) base.GetValue(ButtonTypeProperty);
            set => 
                base.SetValue(ButtonTypeProperty, value);
        }

        public DataPagerDisplayMode DisplayMode
        {
            get => 
                (DataPagerDisplayMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }

        public bool IsCurrentPage
        {
            get => 
                (bool) base.GetValue(IsCurrentPageProperty);
            set => 
                base.SetValue(IsCurrentPageProperty, value);
        }

        public bool ShowEllipsis
        {
            get => 
                (bool) base.GetValue(ShowEllipsisProperty);
            set => 
                base.SetValue(ShowEllipsisProperty, value);
        }

        public int PageNumber
        {
            get => 
                (int) base.GetValue(PageNumberProperty);
            set => 
                base.SetValue(PageNumberProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataPagerButton.<>c <>9 = new DataPagerButton.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerButton) d).OnDisplayModeChanged((DataPagerDisplayMode) e.OldValue);
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerButton) d).OnIsCurrentPageChanged((bool) e.OldValue);
            }
        }
    }
}

