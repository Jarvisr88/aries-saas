namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BooleanChoiceEditorBehavior : Behavior<BaseEdit>
    {
        public static readonly DependencyProperty IsThreeStateProperty;
        public static readonly DependencyProperty DefaultNameProperty;
        public static readonly DependencyProperty TrueNameProperty;
        public static readonly DependencyProperty FalseNameProperty;

        static BooleanChoiceEditorBehavior()
        {
            IsThreeStateProperty = DependencyProperty.Register("IsThreeState", typeof(bool), typeof(BooleanChoiceEditorBehavior), new PropertyMetadata(true, (d, e) => ((BooleanChoiceEditorBehavior) d).UpdateItemsSource()));
            DefaultNameProperty = DependencyProperty.Register("DefaultName", typeof(string), typeof(BooleanChoiceEditorBehavior), new PropertyMetadata(null, (d, e) => ((BooleanChoiceEditorBehavior) d).UpdateItemsSource()));
            TrueNameProperty = DependencyProperty.Register("TrueName", typeof(string), typeof(BooleanChoiceEditorBehavior), new PropertyMetadata(null, (d, e) => ((BooleanChoiceEditorBehavior) d).UpdateItemsSource()));
            FalseNameProperty = DependencyProperty.Register("FalseName", typeof(string), typeof(BooleanChoiceEditorBehavior), new PropertyMetadata(null, (d, e) => ((BooleanChoiceEditorBehavior) d).UpdateItemsSource()));
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            TypeDescriptor.GetProperties(base.AssociatedObject)["ValueMember"].SetValue(base.AssociatedObject, "Value");
            TypeDescriptor.GetProperties(base.AssociatedObject)["DisplayMember"].SetValue(base.AssociatedObject, "Name");
            this.UpdateItemsSource();
        }

        private void UpdateItemsSource()
        {
            if (base.AssociatedObject != null)
            {
                List<BooleanInfo> list = new List<BooleanInfo>();
                if (this.IsThreeState)
                {
                    bool? nullable = null;
                    BooleanInfo info1 = new BooleanInfo();
                    info1.Value = nullable;
                    info1.Name = this.DefaultName;
                    list.Add(info1);
                }
                BooleanInfo item = new BooleanInfo();
                item.Value = true;
                item.Name = this.TrueName;
                list.Add(item);
                BooleanInfo info3 = new BooleanInfo();
                info3.Value = false;
                info3.Name = this.FalseName;
                list.Add(info3);
                TypeDescriptor.GetProperties(base.AssociatedObject)["ItemsSource"].SetValue(base.AssociatedObject, list);
            }
        }

        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        public string DefaultName
        {
            get => 
                (string) base.GetValue(DefaultNameProperty);
            set => 
                base.SetValue(DefaultNameProperty, value);
        }

        public string TrueName
        {
            get => 
                (string) base.GetValue(TrueNameProperty);
            set => 
                base.SetValue(TrueNameProperty, value);
        }

        public string FalseName
        {
            get => 
                (string) base.GetValue(FalseNameProperty);
            set => 
                base.SetValue(FalseNameProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BooleanChoiceEditorBehavior.<>c <>9 = new BooleanChoiceEditorBehavior.<>c();

            internal void <.cctor>b__20_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BooleanChoiceEditorBehavior) d).UpdateItemsSource();
            }

            internal void <.cctor>b__20_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BooleanChoiceEditorBehavior) d).UpdateItemsSource();
            }

            internal void <.cctor>b__20_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BooleanChoiceEditorBehavior) d).UpdateItemsSource();
            }

            internal void <.cctor>b__20_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BooleanChoiceEditorBehavior) d).UpdateItemsSource();
            }
        }

        private class BooleanInfo
        {
            public string Name { get; set; }

            public bool? Value { get; set; }
        }
    }
}

