namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class BackgroundPanel : ContentControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty PropertyWasModifiedByBackgroundPanelProperty = DependencyProperty.RegisterAttached("PropertyWasModifiedByBackgroundPanel", typeof(bool), typeof(BackgroundPanel), new UIPropertyMetadata(false));
        private bool isFirstMeasure = true;
        private List<Type> stopList;
        private List<Type> specialList;

        static BackgroundPanel()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(BackgroundPanel), new FrameworkPropertyMetadata(typeof(BackgroundPanel)));
            ContentControl.ContentProperty.AddOwner(typeof(BackgroundPanel), new FrameworkPropertyMetadata(new PropertyChangedCallback(BackgroundPanel.OnUpdateProperty)));
            TextBlock.ForegroundProperty.AddOwner(typeof(BackgroundPanel), new FrameworkPropertyMetadata(new PropertyChangedCallback(BackgroundPanel.OnUpdateProperty)));
            TextBlock.FontFamilyProperty.AddOwner(typeof(BackgroundPanel), new FrameworkPropertyMetadata(new PropertyChangedCallback(BackgroundPanel.OnUpdateProperty)));
            TextBlock.FontSizeProperty.AddOwner(typeof(BackgroundPanel), new FrameworkPropertyMetadata(new PropertyChangedCallback(BackgroundPanel.OnUpdateProperty)));
        }

        public BackgroundPanel()
        {
            Type[] collection = new Type[] { typeof(RichTextBox), typeof(TextEditBase) };
            this.stopList = new List<Type>(collection);
            Type[] typeArray2 = new Type[] { typeof(DXTabControl) };
            this.specialList = new List<Type>(typeArray2);
        }

        private void BeginCorrectProperties()
        {
            this.DelayedExecute(() => this.CorrectProperties());
        }

        private bool CanSetParentPropertyValue(FrameworkElement parent, FrameworkElement child, DependencyProperty dp)
        {
            bool flag = false;
            object defaultValue = dp.GetMetadata(parent).DefaultValue;
            object obj3 = parent.GetValue(dp);
            if ((obj3 != null) && (defaultValue != null))
            {
                flag = this.ComparePropertyValues(dp, defaultValue, obj3);
            }
            return ((parent.GetValue(dp) != null) && (!(child is TextBox) && !flag));
        }

        protected virtual void ClearValue(DependencyObject target, DependencyProperty dp)
        {
            target.ClearValue(dp);
        }

        private bool ComparePropertyValues(DependencyProperty dp, object object1, object object2)
        {
            if (object2 == object1)
            {
                return true;
            }
            if (ReferenceEquals(dp, TextBlock.FontSizeProperty))
            {
                return (((double) object1) == ((double) object2));
            }
            if (ReferenceEquals(dp, TextBlock.FontFamilyProperty))
            {
                FontFamily family = object1 as FontFamily;
                FontFamily family2 = object2 as FontFamily;
                if ((family != null) && ((family2 != null) && (family.Source == family2.Source)))
                {
                    return true;
                }
            }
            if (!ReferenceEquals(dp, TextBlock.ForegroundProperty))
            {
                return false;
            }
            SolidColorBrush brush = object1 as SolidColorBrush;
            SolidColorBrush brush2 = object2 as SolidColorBrush;
            return ((brush != null) && ((brush2 != null) && (brush.Color == brush2.Color)));
        }

        private bool CorrectProperties()
        {
            FrameworkElement contentPresenter = LayoutHelper.FindElementByName(this, "contentPresenter");
            if (contentPresenter == null)
            {
                return false;
            }
            FrameworkElement child = LayoutHelper.FindElement(contentPresenter, e => (e != null) && !ReferenceEquals(e, contentPresenter));
            if (child == null)
            {
                return false;
            }
            if (!this.stopList.Any<Type>(x => child.GetType().IsAssignableFrom(x)))
            {
                FrameworkElement parent = base.Parent as FrameworkElement;
                if (this.specialList.Any<Type>(x => child.GetType().IsAssignableFrom(x)))
                {
                    if (parent == null)
                    {
                        this.SetOwnPropertyValues(child, prop => this.SetOwnPropertyValuesCoreForSpecialControl(child, prop));
                    }
                    return true;
                }
                if (parent == null)
                {
                    this.SetOwnPropertyValues(child, prop => this.SetOwnPropertyValuesCore(child, prop));
                }
                else
                {
                    bool flag2 = this.CorrectPropertyCore(child, parent, TextBlock.FontFamilyProperty);
                    bool flag3 = this.CorrectPropertyCore(child, parent, TextBlock.FontSizeProperty);
                    if (!this.CorrectPropertyCore(child, parent, TextBlock.ForegroundProperty) && (!flag2 && !flag3))
                    {
                        child.ClearValue(PropertyWasModifiedByBackgroundPanelProperty);
                    }
                    else
                    {
                        child.SetValue(PropertyWasModifiedByBackgroundPanelProperty, true);
                    }
                }
            }
            return true;
        }

        private bool CorrectPropertyCore(FrameworkElement child, FrameworkElement parent, DependencyProperty dp)
        {
            bool flag = false;
            if (ReferenceEquals(dp, TextBlock.ForegroundProperty))
            {
                if (!this.CanSetParentPropertyValue(parent, child, dp))
                {
                    flag = this.SetOwnPropertyValuesCore(child, dp);
                }
                else
                {
                    this.SetValue(child, dp, parent.GetValue(dp));
                    flag = true;
                }
            }
            else
            {
                object defaultValue = dp.GetMetadata(parent).DefaultValue;
                object obj3 = parent.GetValue(dp);
                ValueSource valueSource = System.Windows.DependencyPropertyHelper.GetValueSource(parent, dp);
                if (!this.ComparePropertyValues(dp, defaultValue, child.GetValue(dp)) && (valueSource.BaseValueSource != BaseValueSource.DefaultStyle))
                {
                    if ((bool) child.GetValue(PropertyWasModifiedByBackgroundPanelProperty))
                    {
                        this.ClearValue(child, dp);
                    }
                }
                else
                {
                    if (!this.ComparePropertyValues(dp, defaultValue, obj3))
                    {
                        this.SetValue(child, dp, parent.GetValue(dp));
                    }
                    else
                    {
                        this.SetValue(child, dp, base.GetValue(dp));
                    }
                    flag = true;
                }
            }
            return flag;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = base.MeasureOverride(constraint);
            if (this.isFirstMeasure)
            {
                this.isFirstMeasure = false;
                if (!this.CorrectProperties())
                {
                    this.BeginCorrectProperties();
                }
            }
            return size;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.BeginCorrectProperties();
            this.isFirstMeasure = true;
        }

        protected static void OnUpdateProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BackgroundPanel panel = d as BackgroundPanel;
            if (panel != null)
            {
                panel.BeginCorrectProperties();
            }
        }

        private void SetOwnPropertyValues(FrameworkElement child, Func<DependencyProperty, bool> setChildProperty)
        {
            bool flag2 = setChildProperty(TextBlock.FontFamilyProperty);
            bool flag3 = setChildProperty(TextBlock.FontSizeProperty);
            if (!setChildProperty(TextBlock.ForegroundProperty) && (!flag2 && !flag3))
            {
                child.ClearValue(PropertyWasModifiedByBackgroundPanelProperty);
            }
            else
            {
                child.SetValue(PropertyWasModifiedByBackgroundPanelProperty, true);
            }
        }

        private bool SetOwnPropertyValuesCore(FrameworkElement child, DependencyProperty dp)
        {
            bool flag = false;
            if ((base.GetValue(dp) == null) || (child is TextBox))
            {
                this.ClearValue(child, dp);
            }
            else
            {
                this.SetValue(child, dp, base.GetValue(dp));
                flag = true;
            }
            return flag;
        }

        private bool SetOwnPropertyValuesCoreForSpecialControl(FrameworkElement child, DependencyProperty dp)
        {
            if (base.GetValue(dp) == null)
            {
                return false;
            }
            ValueSource valueSource = System.Windows.DependencyPropertyHelper.GetValueSource(child, dp);
            if ((valueSource.BaseValueSource != BaseValueSource.Default) && (valueSource.BaseValueSource != BaseValueSource.Inherited))
            {
                return false;
            }
            child.SetCurrentValue(dp, base.GetValue(dp));
            return true;
        }

        protected virtual void SetValue(DependencyObject target, DependencyProperty dp, object value)
        {
            target.SetValue(dp, value);
        }
    }
}

