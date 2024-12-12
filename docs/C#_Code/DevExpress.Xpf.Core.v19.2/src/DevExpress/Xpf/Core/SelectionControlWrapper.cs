namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public abstract class SelectionControlWrapper
    {
        private static Dictionary<Type, Type> wrappers;

        static SelectionControlWrapper()
        {
            Dictionary<Type, Type> dictionary = new Dictionary<Type, Type> {
                { 
                    typeof(ListBox),
                    typeof(ListBoxSelectionControlWrapper)
                },
                { 
                    typeof(ListBoxEdit),
                    typeof(ListEditSelectionControlWrapper)
                }
            };
            wrappers = dictionary;
        }

        protected SelectionControlWrapper()
        {
        }

        public abstract void ClearSelection();
        public static SelectionControlWrapper Create(object source)
        {
            SelectionControlWrapper wrapper;
            using (Dictionary<Type, Type>.Enumerator enumerator = Wrappers.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<Type, Type> current = enumerator.Current;
                        if (!current.Key.IsInstanceOfType(source))
                        {
                            continue;
                        }
                        object[] args = new object[] { source };
                        wrapper = (SelectionControlWrapper) Activator.CreateInstance(current.Value, args);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return wrapper;
        }

        public abstract IList GetSelectedItems();
        public abstract void SelectItem(object item);
        public abstract void SubscribeSelectionChanged(Action<IList, IList> a);
        public abstract void UnselectItem(object item);
        public abstract void UnsubscribeSelectionChanged();

        public static Dictionary<Type, Type> Wrappers =>
            wrappers;
    }
}

