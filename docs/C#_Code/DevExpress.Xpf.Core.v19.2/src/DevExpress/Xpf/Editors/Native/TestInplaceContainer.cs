namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class TestInplaceContainer : UserControl, IComponentConnector
    {
        private const int RowCount = 5;
        private const int ColCount = 5;
        private TestInplaceEditorOwner owner;
        private ImmediateActionsManager immediateActionsManager;
        internal Grid grid;
        private bool _contentLoaded;

        public TestInplaceContainer()
        {
            this.InitializeComponent();
            this.immediateActionsManager = new ImmediateActionsManager(this);
            this.owner = new TestInplaceEditorOwner(this);
            for (int i = 0; i < 5; i++)
            {
                RowDefinition definition1 = new RowDefinition();
                definition1.Height = new GridLength(1.0, GridUnitType.Auto);
                this.grid.RowDefinitions.Add(definition1);
            }
            for (int j = 0; j < 5; j++)
            {
                ColumnDefinition definition2 = new ColumnDefinition();
                definition2.Width = new GridLength(1.0, GridUnitType.Star);
                this.grid.ColumnDefinitions.Add(definition2);
            }
            int num3 = 0;
            while (num3 < 5)
            {
                int num4 = 0;
                while (true)
                {
                    if (num4 >= 5)
                    {
                        num3++;
                        break;
                    }
                    int num5 = num3 + num4;
                    EditableDataObject data = new EditableDataObject();
                    data.Value = num5.ToString();
                    TestInplaceEditor editor = new TestInplaceEditor(this.owner, new TestInplaceEditorColumn((ControlTemplate) base.FindResource("emptyValueTemplate"), data));
                    TestBorder border1 = new TestBorder();
                    border1.BorderThickness = new Thickness(1.0);
                    border1.Child = editor;
                    TestBorder element = border1;
                    Grid.SetRow(element, num3);
                    Grid.SetColumn(element, num4);
                    this.grid.Children.Add(element);
                    num4++;
                }
            }
            base.LayoutUpdated += (<sender>, <e>) => this.immediateActionsManager.ExecuteActions();
        }

        internal void EnqueueImmediateAction(IAction action)
        {
            this.immediateActionsManager.EnqueueAction(action);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/editors/inplaceediting/testinplacecontainer.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            this.owner.ProcessIsKeyboardFocusWithinChanged();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((e.Key == Key.Delete) && ModifierKeysHelper.IsCtrlPressed(e.KeyboardDevice.Modifiers))
            {
                foreach (TestBorder border in this.grid.Children)
                {
                    ((TestInplaceEditor) border.Child).Data.Value = null;
                }
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }

        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnPreviewLostKeyboardFocus(e);
            this.owner.ProcessPreviewLostKeyboardFocus(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            this.owner.ProcessMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            this.owner.ProcessMouseLeftButtonUp(e);
        }

        internal void PerformNavigationOnLeftButtonDown(DependencyObject originalSource)
        {
            TestBorder editor = LayoutHelper.FindLayoutOrVisualParentObject<TestBorder>(originalSource, false, null);
            this.SetFocusedEditor(editor);
        }

        internal void ProcessKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                this.owner.MoveFocus(e);
            }
            else
            {
                TestBorder border = LayoutHelper.FindLayoutOrVisualParentObject<TestBorder>((DependencyObject) e.OriginalSource, false, null);
                int row = Grid.GetRow(border);
                int col = Grid.GetColumn(border);
                switch (e.Key)
                {
                    case Key.Left:
                        col--;
                        e.Handled = true;
                        break;

                    case Key.Up:
                        row--;
                        e.Handled = true;
                        break;

                    case Key.Right:
                        col++;
                        e.Handled = true;
                        break;

                    case Key.Down:
                        row++;
                        e.Handled = true;
                        break;

                    default:
                        break;
                }
                TestBorder editor = this.grid.Children.Cast<TestBorder>().FirstOrDefault<TestBorder>(element => (Grid.GetRow(element) == row) && (Grid.GetColumn(element) == col));
                if (editor != null)
                {
                    this.SetFocusedEditor(editor);
                }
            }
        }

        private void SetFocusedEditor(TestBorder editor)
        {
            foreach (TestBorder border in this.grid.Children)
            {
                ((TestInplaceEditor) border.Child).IsEditorFocused = ReferenceEquals(border, editor);
                border.BorderBrush = ReferenceEquals(border, editor) ? Brushes.Red : null;
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.grid = (Grid) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }
    }
}

