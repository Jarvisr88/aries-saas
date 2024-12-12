namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Utils;
    using DevExpress.Utils;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class GridAssemblyHelper
    {
        private static GridAssemblyHelper instance;
        private IGridManagerFactory gridFactory;
        private const string managerAssembly = "DevExpress.Xpf.Core.ConditionalFormattingManager";
        private const string factoryTypeName = "GridManagerFactory";

        private GridAssemblyHelper()
        {
            this.gridFactory = this.CreateFactory();
        }

        private IGridManagerFactory CreateFactory()
        {
            IGridManagerFactory factory = this.CreateFactory(AssemblyHelper.LoadDXAssembly("DevExpress.Xpf.Grid.v19.2"));
            return this.CreateFactory(Helpers.LoadWithPartialName("DevExpress.Xpf.Grid.v19.2, Version=19.2.9.0"));
        }

        private IGridManagerFactory CreateFactory(Assembly gridAssembly)
        {
            if (gridAssembly == null)
            {
                return null;
            }
            Type type = gridAssembly.GetType($"{"DevExpress.Xpf.Core.ConditionalFormattingManager"}.{"GridManagerFactory"}");
            return ((type != null) ? (Activator.CreateInstance(type) as IGridManagerFactory) : null);
        }

        public UIElement CreateGrid() => 
            this.CreateIfAvailable<UIElement>(() => this.gridFactory.CreateGrid());

        private T CreateIfAvailable<T>(Func<T> creator) where T: class
        {
            if (this.IsGridAvailable)
            {
                return creator();
            }
            return default(T);
        }

        public ContentControl CreatePreviewControl() => 
            this.CreateIfAvailable<ContentControl>(() => this.gridFactory.CreatePreviewControl());

        public static GridAssemblyHelper Instance
        {
            get
            {
                instance ??= new GridAssemblyHelper();
                return instance;
            }
        }

        public bool IsGridAvailable =>
            this.gridFactory != null;
    }
}

