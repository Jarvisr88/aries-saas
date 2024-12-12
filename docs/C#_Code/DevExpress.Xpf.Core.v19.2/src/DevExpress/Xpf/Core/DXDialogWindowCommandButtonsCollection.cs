namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class DXDialogWindowCommandButtonsCollection : ObservableCollection<UIElement>
    {
        private Func<UICommand, UIElement> CreateElement;
        private Action<UIElement> ClearElement;
        private IEnumerable<UICommand> Commands;
        private bool allowCollectionChanged = true;

        internal void ClearSource()
        {
            if (this.Commands != null)
            {
                if (this.Commands is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged) this.Commands).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCommandsCollectionChanged);
                }
                this.allowCollectionChanged = true;
                foreach (UIElement element in this)
                {
                    this.ClearElement(element);
                }
                base.Clear();
                this.Commands = null;
                this.CreateElement = null;
                this.ClearElement = null;
            }
        }

        private static Button FindButton(DXDialogWindowCommandButtonsCollection buttons, UICommand command)
        {
            Button button2;
            if (command == null)
            {
                return null;
            }
            using (IEnumerator<UIElement> enumerator = buttons.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        UIElement current = enumerator.Current;
                        Button button = current as Button;
                        if (button == null)
                        {
                            continue;
                        }
                        DXDialogWindowUICommandWrapper dataContext = button.DataContext as DXDialogWindowUICommandWrapper;
                        if ((dataContext == null) || !ReferenceEquals(dataContext.UICommand, command))
                        {
                            continue;
                        }
                        button2 = button;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return button2;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!this.allowCollectionChanged)
            {
                throw new InvalidOperationException("Cannot change CommandButtons collection if CommandSource is set.");
            }
            base.OnCollectionChanged(e);
        }

        private void OnCommandsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.Commands is IList)
            {
                this.allowCollectionChanged = true;
                Func<object, object> convertItemAction = p => this.CreateElement((UICommand) p);
                SyncCollectionHelper.SyncCollection(e, this, (IList) this.Commands, convertItemAction, null, null, null);
                this.allowCollectionChanged = false;
            }
        }

        internal void SetSource(IEnumerable<UICommand> commands, Func<UICommand, UIElement> createElement, Action<UIElement> clearElement, bool doNotGenerateButtons)
        {
            if ((this.Commands == null) && (base.Count > 0))
            {
                throw new InvalidOperationException("Cannot use CommandsSource if CommandButtons collection is not empty.");
            }
            this.ClearSource();
            this.Commands = commands;
            this.CreateElement = createElement;
            this.ClearElement = clearElement;
            if (this.Commands != null)
            {
                Func<object, object> convertItemAction = p => this.CreateElement((UICommand) p);
                SyncCollectionHelper.PopulateCore(this, doNotGenerateButtons ? Enumerable.Empty<UICommand>() : this.Commands, convertItemAction, null, null);
                this.allowCollectionChanged = false;
                if (!doNotGenerateButtons && (this.Commands is INotifyCollectionChanged))
                {
                    ((INotifyCollectionChanged) this.Commands).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCommandsCollectionChanged);
                }
            }
        }

        public Button this[UICommand command] =>
            FindButton(this, command);
    }
}

