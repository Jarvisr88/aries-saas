namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TemplatePart(Name="EditorElement", Type=typeof(TextBox)), TemplateVisualState(GroupName="EditingStates", Name="NonEditable"), TemplateVisualState(GroupName="EditingStates", Name="Editable"), TemplateVisualState(GroupName="EditingStates", Name="Editing")]
    public class TileGroupHeader : ContentControlBase
    {
        public static readonly DependencyProperty StateProperty;
        private const string EditorElementName = "EditorElement";
        private const string EditingStates = "EditingStates";
        private const string NonEditableState = "NonEditable";
        private const string EditableState = "Editable";
        private const string EditingState = "Editing";

        static TileGroupHeader()
        {
            StateProperty = DependencyProperty.Register("State", typeof(TileGroupHeaderState), typeof(TileGroupHeader), new PropertyMetadata((o, e) => ((TileGroupHeader) o).OnStateChanged()));
        }

        public TileGroupHeader()
        {
            base.DefaultStyleKey = typeof(TileGroupHeader);
        }

        private string GetStateName(TileGroupHeaderState state)
        {
            switch (state)
            {
                case TileGroupHeaderState.NonEditable:
                    return "NonEditable";

                case TileGroupHeaderState.Editable:
                    return "Editable";

                case TileGroupHeaderState.Editing:
                    return "Editing";
            }
            return null;
        }

        public override void OnApplyTemplate()
        {
            if (this.EditorElement != null)
            {
                this.EditorElement.GotFocus -= new RoutedEventHandler(this.OnEditorElementGotFocus);
                this.EditorElement.KeyDown -= new KeyEventHandler(this.OnEditorElementKeyDown);
                this.EditorElement.LostFocus -= new RoutedEventHandler(this.OnEditorElementLostFocus);
            }
            base.OnApplyTemplate();
            this.EditorElement = base.GetTemplateChild("EditorElement") as TextBox;
            if (this.EditorElement != null)
            {
                this.EditorElement.GotFocus += new RoutedEventHandler(this.OnEditorElementGotFocus);
                this.EditorElement.KeyDown += new KeyEventHandler(this.OnEditorElementKeyDown);
                this.EditorElement.LostFocus += new RoutedEventHandler(this.OnEditorElementLostFocus);
            }
            this.UpdateEditor();
        }

        protected override void OnContentChanged(object oldValue, object newValue)
        {
            base.OnContentChanged(oldValue, newValue);
            this.UpdateEditor();
        }

        private void OnEditorElementGotFocus(object sender, RoutedEventArgs e)
        {
            this.State = TileGroupHeaderState.Editing;
        }

        private void OnEditorElementKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Return) || (e.Key == Key.Escape))
            {
                this.StopEditing(e.Key == Key.Return);
            }
        }

        private void OnEditorElementLostFocus(object sender, RoutedEventArgs e)
        {
            this.StopEditing(true);
        }

        protected virtual void OnStateChanged()
        {
            this.UpdateState(false);
            this.UpdateEditor();
        }

        internal void StopEditing(bool accept)
        {
            if (this.State == TileGroupHeaderState.Editing)
            {
                if (accept)
                {
                    this.Content = (this.EditorElement.Text == "") ? null : this.EditorElement.Text;
                }
                this.State = TileGroupHeaderState.Editable;
                this.EditorElement.Select(0, 0);
                DependencyObject focusScope = FocusManager.GetFocusScope(this);
                if (ReferenceEquals(FocusManager.GetFocusedElement(focusScope), this.EditorElement))
                {
                    FocusManager.SetFocusedElement(focusScope, null);
                    Keyboard.ClearFocus();
                }
            }
        }

        private void UpdateEditor()
        {
            if ((this.EditorElement != null) && (this.State != TileGroupHeaderState.NonEditable))
            {
                string content = base.Content as string;
                if (string.IsNullOrEmpty(content))
                {
                    this.EditorElement.Text = (this.State == TileGroupHeaderState.Editable) ? LocalizationRes.TileLayoutControl_GroupHeader_Empty : "";
                }
                else
                {
                    this.EditorElement.Text = content;
                }
            }
        }

        protected override void UpdateState(bool useTransitions)
        {
            base.UpdateState(useTransitions);
            base.GoToState(this.GetStateName(this.State), useTransitions);
        }

        public TileGroupHeaderState State
        {
            get => 
                (TileGroupHeaderState) base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        protected TextBox EditorElement { get; private set; }

        protected override bool IsContentInLogicalTree =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TileGroupHeader.<>c <>9 = new TileGroupHeader.<>c();

            internal void <.cctor>b__26_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((TileGroupHeader) o).OnStateChanged();
            }
        }
    }
}

