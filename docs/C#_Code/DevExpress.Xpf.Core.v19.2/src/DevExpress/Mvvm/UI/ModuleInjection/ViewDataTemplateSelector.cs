namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ViewDataTemplateSelector : DataTemplateSelector
    {
        private readonly List<ViewModelInfo> infos = new List<ViewModelInfo>();

        public void Add(object viewModel, Type viewType)
        {
            this.UpdateInfos();
            this.infos.Add(new ViewModelInfo(viewModel, viewType));
        }

        private DataTemplate GetViewTemplate(object viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }
            this.UpdateInfos();
            ViewModelInfo info = this.infos.FirstOrDefault<ViewModelInfo>(x => x.ViewModel.Target == viewModel);
            if (info == null)
            {
                return null;
            }
            info.UpdateViewTemplate();
            return info.ViewTemplate;
        }

        public void Remove(object viewModel)
        {
            this.UpdateInfos();
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) => 
            this.GetViewTemplate(item) ?? base.SelectTemplate(item, container);

        private void UpdateInfos()
        {
            List<ViewModelInfo> list = new List<ViewModelInfo>();
            foreach (ViewModelInfo info in this.infos)
            {
                if (!info.ViewModel.IsAlive)
                {
                    list.Add(info);
                }
            }
            foreach (ViewModelInfo info2 in list)
            {
                this.infos.Remove(info2);
            }
        }

        private class ViewModelInfo
        {
            public ViewModelInfo(object viewModel, Type viewType)
            {
                this.ViewModel = new WeakReference(viewModel);
                this.ViewType = viewType;
            }

            public void UpdateViewTemplate()
            {
                if (this.ViewTemplate == null)
                {
                    if (this.ViewType != null)
                    {
                        this.ViewTemplate = ViewLocatorExtensions.CreateViewTemplate(this.ViewType);
                    }
                    else
                    {
                        this.ViewTemplate = null;
                    }
                }
            }

            public WeakReference ViewModel { get; private set; }

            public Type ViewType { get; private set; }

            public DataTemplate ViewTemplate { get; private set; }
        }
    }
}

