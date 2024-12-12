namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;

    public class LogicalContainer<T> : FrameworkElement where T: DependencyObject
    {
        private readonly List<T> Children;

        public LogicalContainer()
        {
            this.Children = new List<T>();
        }

        public void Add(T item)
        {
            if (this.IsNotInTree(item))
            {
                if (!this.Children.Contains(item))
                {
                    this.Children.Add(item);
                }
                this.AddCore(item);
            }
        }

        private void AddCore(T item)
        {
            base.AddLogicalChild(item);
        }

        private bool IsNotInTree(T item) => 
            ReferenceEquals(LogicalTreeHelper.GetParent(item), null);

        public void Remove(T item)
        {
            this.Children.Remove(item);
            this.RemoveCore(item);
        }

        private void RemoveCore(T item)
        {
            base.RemoveLogicalChild(item);
        }

        protected override IEnumerator LogicalChildren =>
            this.Children.GetEnumerator();
    }
}

