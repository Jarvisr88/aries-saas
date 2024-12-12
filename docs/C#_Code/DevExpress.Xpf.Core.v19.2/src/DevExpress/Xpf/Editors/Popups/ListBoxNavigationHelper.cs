namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ListBoxNavigationHelper
    {
        public ListBoxNavigationHelper(System.Windows.Controls.ListBox listBox)
        {
            this.ListBox = listBox;
        }

        protected virtual bool CanSelectItem(object item)
        {
            UIElement element = item as UIElement;
            return ((element == null) || UIElementHelper.IsEnabled(element));
        }

        private void DoNavigationAction(bool down, Action upAction, Action downAction)
        {
            if (down)
            {
                downAction();
            }
            else
            {
                upAction();
            }
        }

        private ScrollViewer Find(DependencyObject obj)
        {
            while (obj != null)
            {
                ScrollViewer viewer = obj as ScrollViewer;
                if (viewer != null)
                {
                    return viewer;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void FocusContainer(DependencyObject container)
        {
            Action<ListBoxItem> action = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Action<ListBoxItem> local1 = <>c.<>9__21_0;
                action = <>c.<>9__21_0 = x => x.Focus();
            }
            (container as ListBoxItem).Do<ListBoxItem>(action);
        }

        protected virtual void Move(int startIndex, int stopIndex, int delta)
        {
            if (this.Items.Count != 0)
            {
                for (int i = startIndex; i != stopIndex; i += delta)
                {
                    object item = this.Items[i];
                    DependencyObject container = this.ListBox.ItemContainerGenerator.ContainerFromIndex(i);
                    if (this.CanSelectItem(item) && this.CanSelectItem(container))
                    {
                        this.ListBox.SelectedIndex = i;
                        Func<System.Windows.Controls.ListBox, bool> evaluator = <>c.<>9__15_0;
                        if (<>c.<>9__15_0 == null)
                        {
                            Func<System.Windows.Controls.ListBox, bool> local1 = <>c.<>9__15_0;
                            evaluator = <>c.<>9__15_0 = x => x.IsKeyboardFocusWithin;
                        }
                        this.ListBox.If<System.Windows.Controls.ListBox>(evaluator).Do<System.Windows.Controls.ListBox>(x => this.FocusContainer(container));
                        return;
                    }
                }
            }
        }

        public void MoveFirst()
        {
            this.Move(0, this.Items.Count, 1);
        }

        private void MoveFocus(int index)
        {
            if (this.Items.Count != 0)
            {
                object item = this.Items[index];
                if (this.CanSelectItem(item))
                {
                    ((EditorListBox) this.ListBox).MakeVisibleAndFocusItem(item);
                }
            }
        }

        public void MoveFocusFirst()
        {
            this.MoveFocus(0);
        }

        public void MoveFocusLast()
        {
            this.MoveFocus(this.Items.Count - 1);
        }

        public void MoveFocusNext()
        {
            this.MoveFocus(Math.Min((int) (this.FocusedItemIndex + 1), (int) (this.Items.Count - 1)));
        }

        public void MoveFocusPrev()
        {
            this.MoveFocus(Math.Max(0, this.FocusedItemIndex - 1));
        }

        public void MoveLast()
        {
            this.Move(this.Items.Count - 1, -1, -1);
        }

        public void MoveNext()
        {
            this.Move(this.SelectedIndex + 1, this.Items.Count, 1);
        }

        protected virtual void MovePage(bool down)
        {
            int index = (this.SelectedIndex < 0) ? 0 : this.SelectedIndex;
            if (index < this.Items.Count)
            {
                ScrollViewer viewer = this.Find(this.ListBox.ItemContainerGenerator.ContainerFromIndex(index));
                if (viewer != null)
                {
                    if (!viewer.CanContentScroll)
                    {
                        this.DoNavigationAction(down, new Action(this.MoveFirst), new Action(this.MoveLast));
                    }
                    else
                    {
                        this.DoNavigationAction(down, new Action(viewer.PageUp), new Action(viewer.PageDown));
                        viewer.UpdateLayout();
                        MethodInfo method = typeof(ItemsControl).GetMethod("GetFirstItemOnCurrentPage", BindingFlags.NonPublic | BindingFlags.Instance);
                        object[] objArray1 = new object[3];
                        objArray1[0] = this.ListBox.ItemContainerGenerator.ContainerFromIndex(index);
                        objArray1[1] = down ? FocusNavigationDirection.Down : FocusNavigationDirection.Up;
                        object[] parameters = objArray1;
                        method.Invoke(this.ListBox, BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, CultureInfo.CurrentCulture);
                        DependencyObject container = parameters[2] as DependencyObject;
                        if (container != null)
                        {
                            int startIndex = this.ListBox.ItemContainerGenerator.IndexFromContainer(container);
                            this.Move(startIndex, -1, -1);
                        }
                    }
                }
            }
        }

        public void MovePageDown()
        {
            this.MovePage(true);
        }

        public void MovePageUp()
        {
            this.MovePage(false);
        }

        public void MovePrev()
        {
            this.Move(Math.Max(0, this.SelectedIndex - 1), -1, -1);
        }

        public System.Windows.Controls.ListBox ListBox { get; private set; }

        protected ItemCollection Items =>
            this.ListBox.Items;

        protected int SelectedIndex =>
            this.ListBox.SelectedIndex;

        private int FocusedItemIndex =>
            Math.Max(0, ((EditorListBox) this.ListBox).GetFocusedItemIndex());

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ListBoxNavigationHelper.<>c <>9 = new ListBoxNavigationHelper.<>c();
            public static Func<ListBox, bool> <>9__15_0;
            public static Action<ListBoxItem> <>9__21_0;

            internal void <FocusContainer>b__21_0(ListBoxItem x)
            {
                x.Focus();
            }

            internal bool <Move>b__15_0(ListBox x) => 
                x.IsKeyboardFocusWithin;
        }
    }
}

