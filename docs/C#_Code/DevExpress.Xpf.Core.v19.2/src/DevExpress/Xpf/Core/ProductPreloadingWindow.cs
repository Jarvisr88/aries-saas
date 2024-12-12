namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class ProductPreloadingWindow : Window
    {
        private readonly Queue<Func<bool>> actions = new Queue<Func<bool>>();
        private readonly ProductPreloading preloading;
        private Func<bool> currentAction;

        public ProductPreloadingWindow(ProductPreloading preloading)
        {
            this.preloading = preloading;
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        private void OnLayoutUpdated(object sender, EventArgs eventArgs)
        {
            if (this.actions.Count != 0)
            {
                this.currentAction ??= this.actions.Dequeue();
                if (this.currentAction())
                {
                    this.currentAction = null;
                }
                base.InvalidateMeasure();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            foreach (ProductPreloading preloading in Enum.GetValues(typeof(ProductPreloading)))
            {
                if (!this.preloading.HasFlag(preloading))
                {
                    continue;
                }
                IProductPreloadingItem valueOrDefault = ProductPreloadingHelper.RegisteredItems.GetValueOrDefault<ProductPreloading, IProductPreloadingItem>(preloading);
                if (valueOrDefault != null)
                {
                    ProductPreloadingHelper.PreloadAssembly(valueOrDefault.AssemblyFullName);
                    foreach (FrameworkElement element in valueOrDefault.Controls)
                    {
                        this.actions.Enqueue(delegate {
                            this.Content = element;
                            DispatcherHelper.UpdateLayoutAndDoEvents(this);
                            return true;
                        });
                        this.actions.Enqueue(() => element.IsLoaded);
                    }
                }
            }
            this.actions.Enqueue(delegate {
                base.Close();
                return true;
            });
            base.InvalidateMeasure();
        }
    }
}

