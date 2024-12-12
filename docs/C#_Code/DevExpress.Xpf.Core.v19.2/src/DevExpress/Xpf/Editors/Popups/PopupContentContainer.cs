namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [TemplateVisualState(Name="BottomToTopDirection", GroupName="DropDownDirectionStates"), TemplateVisualState(Name="TopToBottomDirection", GroupName="DropDownDirectionStates")]
    public class PopupContentContainer : ContentControl
    {
        public static readonly DependencyProperty DropOppositeProperty;

        static PopupContentContainer()
        {
            DropOppositeProperty = DependencyProperty.Register("DropOpposite", typeof(bool), typeof(PopupContentContainer), new PropertyMetadata((d, e) => ((PopupContentContainer) d).DropOppositeChanged()));
        }

        public PopupContentContainer()
        {
        }

        public PopupContentContainer(EditorPopupBase popup)
        {
            Binding binding = new Binding {
                Path = new PropertyPath("DropOpposite", new object[0]),
                Source = ((PopupBaseEditPropertyProvider) ActualPropertyProvider.GetProperties(BaseEdit.GetOwnerEdit(popup))).PopupViewModel
            };
            base.SetBinding(DropOppositeProperty, binding);
        }

        protected virtual void DropOppositeChanged()
        {
            this.UpdateVisualState(true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
        }

        protected internal virtual void UpdateVisualState(bool useTransitions)
        {
            string stateName = this.DropOpposite ? 1.ToString() : 0.ToString();
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        public bool DropOpposite
        {
            get => 
                (bool) base.GetValue(DropOppositeProperty);
            set => 
                base.SetValue(DropOppositeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PopupContentContainer.<>c <>9 = new PopupContentContainer.<>c();

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PopupContentContainer) d).DropOppositeChanged();
            }
        }
    }
}

