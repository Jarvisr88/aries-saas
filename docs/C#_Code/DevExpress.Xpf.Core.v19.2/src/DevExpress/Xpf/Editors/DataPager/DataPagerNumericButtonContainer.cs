namespace DevExpress.Xpf.Editors.DataPager
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class DataPagerNumericButtonContainer : Control
    {
        public static readonly DependencyProperty AutoEllipsisProperty;
        public static readonly DependencyProperty ButtonCountProperty;
        public static readonly DependencyProperty CurrentIndexProperty;
        public static readonly DependencyProperty FirstButtonPageNumberProperty;
        public static readonly DependencyProperty NumericButtonContainerProperty;
        public static readonly DependencyProperty PageCountProperty;
        public static readonly DependencyProperty SecondButtonPageNumberProperty;

        static DataPagerNumericButtonContainer()
        {
            AutoEllipsisProperty = DependencyPropertyManager.Register("AutoEllipsis", typeof(bool), typeof(DataPagerNumericButtonContainer), new PropertyMetadata((d, e) => ((DataPagerNumericButtonContainer) d).OnAutoEllipsisChanged()));
            ButtonCountProperty = DependencyPropertyManager.Register("ButtonCount", typeof(int), typeof(DataPagerNumericButtonContainer), new PropertyMetadata(null));
            CurrentIndexProperty = DependencyPropertyManager.Register("CurrentIndex", typeof(int), typeof(DataPagerNumericButtonContainer), new PropertyMetadata((d, e) => ((DataPagerNumericButtonContainer) d).OnCurrentIndexChanged()));
            FirstButtonPageNumberProperty = DependencyPropertyManager.Register("FirstButtonPageNumber", typeof(int), typeof(DataPagerNumericButtonContainer), new PropertyMetadata((d, e) => ((DataPagerNumericButtonContainer) d).OnFirstButtonPageNumberChanged()));
            NumericButtonContainerProperty = DependencyPropertyManager.RegisterAttached("NumericButtonContainer", typeof(DataPagerNumericButtonContainer), typeof(DataPagerNumericButtonContainer), new PropertyMetadata(null));
            PageCountProperty = DependencyPropertyManager.Register("PageCount", typeof(int), typeof(DataPagerNumericButtonContainer), new PropertyMetadata((d, e) => ((DataPagerNumericButtonContainer) d).OnPageCountChanged()));
            SecondButtonPageNumberProperty = DependencyPropertyManager.Register("SecondButtonPageNumber", typeof(int), typeof(DataPagerNumericButtonContainer), new PropertyMetadata(2, (d, e) => ((DataPagerNumericButtonContainer) d).OnSecondButtonPageNumberChanged()));
        }

        public DataPagerNumericButtonContainer()
        {
            SetNumericButtonContainer(this, this);
            this.SetDefaultStyleKey(typeof(DataPagerNumericButtonContainer));
        }

        private void ChangeButtonProperties(DataPagerButton dpButton, int pageNumber)
        {
            dpButton.PageNumber = pageNumber;
            dpButton.IsCurrentPage = false;
            if (dpButton.PageNumber == (this.CurrentIndex + 1))
            {
                dpButton.IsCurrentPage = true;
            }
        }

        public DataPagerButton CreateNumericButton(int pageNumber)
        {
            DataPagerButton dpButton = new DataPagerNumericButton();
            DevExpress.Xpf.Editors.DataPager.DataPager pager = LayoutHelper.FindParentObject<DevExpress.Xpf.Editors.DataPager.DataPager>(this);
            if (pager != null)
            {
                dpButton.Command = pager.NumericPageCommand;
            }
            Binding binding = new Binding("PageNumber");
            binding.Source = dpButton;
            dpButton.SetBinding(ButtonBase.CommandParameterProperty, binding);
            this.ChangeButtonProperties(dpButton, pageNumber);
            dpButton.ButtonType = DataPagerButtonType.PageNumeric;
            dpButton.ShowEllipsis = false;
            if (((this.Panel != null) && (((pageNumber == this.FirstButtonPageNumber) && (pageNumber > 1)) || ((pageNumber == ((this.FirstButtonPageNumber + this.Panel.Children.Count) - 1)) && (pageNumber < this.PageCount)))) && this.AutoEllipsis)
            {
                dpButton.ShowEllipsis = true;
            }
            return dpButton;
        }

        internal DataPagerButton GetButton(int index) => 
            (DataPagerButton) this.Panel.Children[index];

        public static DataPagerNumericButtonContainer GetNumericButtonContainer(DependencyObject obj) => 
            (DataPagerNumericButtonContainer) obj.GetValue(NumericButtonContainerProperty);

        public DataPagerNumericButtonContainerPanel GetPanel() => 
            this.Panel as DataPagerNumericButtonContainerPanel;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Panel = base.GetTemplateChild("PART_Panel") as System.Windows.Controls.Panel;
            if (this.Panel != null)
            {
                this.UpdateButtons();
            }
        }

        protected virtual void OnAutoEllipsisChanged()
        {
            this.UpdateButtons();
        }

        protected virtual void OnCurrentIndexChanged()
        {
            this.UpdateButtons();
        }

        protected virtual void OnFirstButtonPageNumberChanged()
        {
            if (this.ButtonCount != 2)
            {
                this.UpdateButtons();
            }
        }

        protected virtual void OnPageCountChanged()
        {
            this.UpdateButtons();
            if (this.Panel != null)
            {
                this.Panel.InvalidateMeasure();
            }
        }

        protected virtual void OnSecondButtonPageNumberChanged()
        {
        }

        public static void SetNumericButtonContainer(DependencyObject obj, DataPagerNumericButtonContainer value)
        {
            obj.SetValue(NumericButtonContainerProperty, value);
        }

        public void UpdateButtons()
        {
            if (this.Panel != null)
            {
                for (int i = 0; i < this.Panel.Children.Count; i++)
                {
                    DataPagerButton dpButton = this.GetButton(i);
                    if (dpButton != null)
                    {
                        dpButton.ShowEllipsis = false;
                        if (this.Panel.Children.Count != 2)
                        {
                            this.ChangeButtonProperties(dpButton, this.FirstButtonPageNumber + i);
                            if ((((i == 0) && (dpButton.PageNumber > 1)) || ((i == (this.Panel.Children.Count - 1)) && (dpButton.PageNumber < this.PageCount))) && this.AutoEllipsis)
                            {
                                dpButton.ShowEllipsis = true;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                this.ChangeButtonProperties(dpButton, this.FirstButtonPageNumber);
                            }
                            else
                            {
                                this.ChangeButtonProperties(dpButton, this.SecondButtonPageNumber);
                            }
                            if ((dpButton.PageNumber > 1) && ((dpButton.PageNumber < this.PageCount) && this.AutoEllipsis))
                            {
                                dpButton.ShowEllipsis = true;
                            }
                        }
                    }
                }
            }
        }

        public bool AutoEllipsis
        {
            get => 
                (bool) base.GetValue(AutoEllipsisProperty);
            set => 
                base.SetValue(AutoEllipsisProperty, value);
        }

        public int ButtonCount
        {
            get => 
                (int) base.GetValue(ButtonCountProperty);
            set => 
                base.SetValue(ButtonCountProperty, value);
        }

        public int CurrentIndex
        {
            get => 
                (int) base.GetValue(CurrentIndexProperty);
            set => 
                base.SetValue(CurrentIndexProperty, value);
        }

        public int FirstButtonPageNumber
        {
            get => 
                (int) base.GetValue(FirstButtonPageNumberProperty);
            set => 
                base.SetValue(FirstButtonPageNumberProperty, value);
        }

        public int PageCount
        {
            get => 
                (int) base.GetValue(PageCountProperty);
            set => 
                base.SetValue(PageCountProperty, value);
        }

        public int SecondButtonPageNumber
        {
            get => 
                (int) base.GetValue(SecondButtonPageNumberProperty);
            set => 
                base.SetValue(SecondButtonPageNumberProperty, value);
        }

        public System.Windows.Controls.Panel Panel { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataPagerNumericButtonContainer.<>c <>9 = new DataPagerNumericButtonContainer.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainer) d).OnAutoEllipsisChanged();
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainer) d).OnCurrentIndexChanged();
            }

            internal void <.cctor>b__7_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainer) d).OnFirstButtonPageNumberChanged();
            }

            internal void <.cctor>b__7_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainer) d).OnPageCountChanged();
            }

            internal void <.cctor>b__7_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataPagerNumericButtonContainer) d).OnSecondButtonPageNumberChanged();
            }
        }
    }
}

