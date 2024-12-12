namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    internal class DefaultTemplateChangeNotifier : ContentPresenter
    {
        private static readonly FindTemplateResourceInTreeHandler FindTemplateResourceInTree;
        private readonly HashSet<Type> itemTypes = new HashSet<Type>();
        private readonly ArrayList templateKeys = new ArrayList();

        public event EventHandler NeedsRecalc;

        static DefaultTemplateChangeNotifier()
        {
            int? parametersCount = null;
            FindTemplateResourceInTree = ReflectionHelper.CreateInstanceMethodHandler<FrameworkElement, FindTemplateResourceInTreeHandler>(null, "FindTemplateResourceInTree", BindingFlags.NonPublic | BindingFlags.Static, parametersCount, null, true);
        }

        public DefaultTemplateChangeNotifier()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected override DataTemplate ChooseTemplate()
        {
            if ((this.NeedsRecalc != null) && (this.itemTypes.Count > 0))
            {
                int count = this.templateKeys.Count;
                if (DefaultTemplateSelector.FindTemplateResourceInTree(this, this.templateKeys, count, ref count) != null)
                {
                    this.NeedsRecalc(this, EventArgs.Empty);
                }
            }
            return base.ChooseTemplate();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ChooseTemplate();
        }

        public void RegisterType(Type itemType)
        {
            if ((itemType != null) && this.itemTypes.Add(itemType))
            {
                this.templateKeys.Add(new DataTemplateKey(itemType));
                if (itemType.BaseType == typeof(object))
                {
                    itemType = null;
                }
                this.RegisterType(itemType);
            }
        }

        public void Reset()
        {
            this.templateKeys.Clear();
            this.itemTypes.Clear();
        }

        private delegate object FindTemplateResourceInTreeHandler(DependencyObject target, ArrayList keys, int exactMatch, ref int bestMatch);
    }
}

